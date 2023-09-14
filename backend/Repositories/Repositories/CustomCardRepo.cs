using System.Timers;
using Microsoft.EntityFrameworkCore;
using Repositories.DAL;
using Repositories.Interfaces;
using Repositories.Models;
using Repositories.Utility;

namespace Repositories.Repositories;

public class CustomCardRepo : Repository<CustomCard>, ICustomCardRepo
{
    public CustomCardRepo(DataContext _ctx) : base(_ctx) { 
        Utils.Timer.Elapsed += AutomaticCardSet;
    }

    public async override Task<ICollection<CustomCard>> GetAll() {
        
        return await table.Include(x => x.LikedUsers).ToListAsync();
    }

    public async override Task<CustomCard> Create(CustomCard? card)
    {
        if (card is null) throw new ArgumentNullException();

        await table.AddAsync(card);

        if (card.FileSteam is null) throw new ArgumentException("The image was not readable");
        if ((card.OwnerAccount.Permissions & Enums.UserPermissions.SubmitCustomCard) == 0) throw new NotAuthorizedException("You do not have permissions for this");

        await SaveCard(card);
        await SaveAsync();
        return card;
    }

    async Task SaveCard(CustomCard card)
    {
        string fileName = card.Id.ToString();
        using var stream = File.Create(Utils.CUSTOM_CARD_PATH + fileName);
        await card.FileSteam!.CopyToAsync(stream);
    }

    public async Task<CustomCardOTD> SetCustomCardOTD(Guid cardId, UserAccount? account = null, CustomCard? card = null)
    {
        card ??= await table.FindAsync(cardId);
        if (card is null) throw new KeyNotFoundException("That card was not found!");

        CustomCardOTD ccOTD;

        if (account is null) 
            ccOTD = new CustomCardOTD(card, true);
        else {
            if ((account.Permissions & Enums.UserPermissions.SetCustomCardOfTheDay) == 0) throw new NotAuthorizedException("You do not have permissions for this!");
            ccOTD = new CustomCardOTD(card, false, account);
        }

        await ctx.CustomCardsOTD.AddAsync(ccOTD);
        await SaveAsync();
        Utils.LAST_CARDOTD_SET = DateTime.Now;
        return ccOTD;
    }

    public async Task<CustomCard> ValidateCustomCard(Guid cardId, UserAccount account, Enums.CustomCardApprovalState state)
    {
        CustomCard? card = await table.FindAsync(cardId);
        if (card is null) throw new KeyNotFoundException("That card was not found!");
        
        if ((account.Permissions & Enums.UserPermissions.ValidateCustomCard) == 0) throw new NotAuthorizedException("You do not have permissions for this!");

        card.State = state;
        await SaveAsync();
        return card;
    }

    private async void AutomaticCardSet(object? sender, ElapsedEventArgs e)
    {
        if (DateTime.Now - Utils.LAST_CARDOTD_SET >= Utils.AUTOMATIC_CARDOTD_DELAY) {
            IEnumerable<CustomCard> cards = (await GetAll()).Where(c => !ctx.CustomCardsOTD.Select(cotd => cotd.Card).Contains(c));

            if (cards.Count() == 0) return;

            CustomCard newCardOTD = cards.OrderBy(x => x.LikedUsers.Count).First();
            await SetCustomCardOTD(newCardOTD.Id, card: newCardOTD);
        }
    }

    public async Task<ICollection<CustomCardOTD>> GetAllCustomCardsOTD()
    {
        return await ctx.CustomCardsOTD.ToListAsync();
    }

    public async Task<CustomCardOTD> GetLastCustomCardOTD()
    {
        return await ctx.CustomCardsOTD.FirstAsync();
    }

    public async Task LikeACard(Guid id, UserAccount account) {
        CustomCard? card = await table.FindAsync(id);
        if (card is null) throw new KeyNotFoundException("Card was not found");

        card.LikedUsers.Add(account);
        await SaveAsync();
    }

    // SUSCRIBE TO ELAPSED EVENT FROM UTILS!!!, SAME FOR DECKOTD

}
