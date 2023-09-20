using System.Timers;
using Microsoft.EntityFrameworkCore;
using Repositories.DAL;
using Repositories.Interfaces;
using Repositories.Models;
using Repositories.Utility;

namespace Repositories.Repositories;

public class CustomCardRepo : Repository<CustomCard>, ICustomCardRepo
{
    static bool subscribed = false;

    public CustomCardRepo(DataContext _ctx) : base(_ctx) { 
        if (!subscribed) {
            Utils.CardTimer.Elapsed += AutomaticCardSet;
            subscribed = true;
        }
    }

    public async override Task<CustomCard> GetById(Guid id) {
        CustomCard? card = await table
            .Include(x => x.LikedUsers)
            .Include(x => x.OwnerAccount)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (card is null) throw new KeyNotFoundException("The card was not found in the database");

        return card;
    }

    public async override Task<CustomCard> Create(CustomCard card)
    {
        if (card.FileSteam is null) throw new ArgumentException("The image was not readable");

        await table.AddAsync(card);
        
        await SaveCard(card);
        await SaveAsync();
        return card;
    }

    async Task SaveCard(CustomCard card)
    {
        string fileName = card.Id.ToString() + ".png";
        using var stream = File.Create(Utils.CUSTOM_CARD_PATH + fileName);
        await card.FileSteam.CopyToAsync(stream);
    }

    public async Task<CustomCardOTD> SetCustomCardOTD(Guid cardId, UserAccount? account = null, CustomCard? card = null)
    {
        card ??= await table.FindAsync(cardId);
        if (card is null) throw new KeyNotFoundException("That card was not found!");

        CustomCardOTD ccOTD;

        if (account is null) 
            ccOTD = new CustomCardOTD(card);
        else {
            if ((account.Permissions & Enums.UserPermissions.SetCustomCardOfTheDay) == 0) throw new NotAuthorizedException("You do not have permissions for this!");
            ccOTD = new CustomCardOTD(card, account);
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
        
        card.State = state;
        await SaveAsync();
        return card;
    }

    public async Task<ICollection<CustomCardOTD>> GetAllCustomCardsOTD()
    {
        return await ctx.CustomCardsOTD
            .Include(x => x.OwnerAccount)
            .Include(x => x.CardSetter)
            .Include(x => x.LikedUsers)
            .ToListAsync();
    }

    public async Task<CustomCardOTD> GetLastCustomCardOTD()
    {
        return await ctx.CustomCardsOTD
            .Include(x => x.OwnerAccount)
            .Include(x => x.CardSetter)
            .Include(x => x.LikedUsers)
            .FirstAsync();
    }

    public async Task LikeACard(Guid id, UserAccount account) {
        CustomCard? card = await table.Include(x => x.LikedUsers).FirstOrDefaultAsync(x => x.Id == id);
        if (card is null) throw new KeyNotFoundException("Card was not found");

        if (card.LikedUsers.Contains(account)) { card.LikedUsers.Remove(account); card.NumberOfLikes -= 1; }
        else { card.LikedUsers.Add(account); card.NumberOfLikes += 1; }
        
        await SaveAsync();
    }

    private async void AutomaticCardSet(object? sender, ElapsedEventArgs e)
    {
        if (DateTime.Now - Utils.LAST_CARDOTD_SET >= Utils.AUTOMATIC_CARDOTD_DELAY) {
            IEnumerable<CustomCard> cards = (await GetAll()).Where(c => !ctx.CustomCardsOTD.Select(cotd => cotd.Id).Contains(c.Id));

            if (cards.Count() == 0) return;

            CustomCard newCardOTD = cards.OrderBy(x => x.NumberOfLikes).First();
            await SetCustomCardOTD(newCardOTD.Id, card: newCardOTD);
            Utils.LAST_CARDOTD_SET = DateTime.Now;
        }
    }
}
