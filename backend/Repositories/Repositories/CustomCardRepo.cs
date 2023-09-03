using Repositories.DAL;
using Repositories.Interfaces;
using Repositories.Models;
using Repositories.Utils;

namespace Repositories.Repositories;

public class CustomCardRepo : Repository<CustomCard>, ICustomCardRepo
{
    public CustomCardRepo(DataContext _ctx) : base(_ctx) { }

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
        using var stream = File.Create(Utils.Utils.CUSTOM_CARD_PATH + fileName);
        await card.FileSteam!.CopyToAsync(stream);
    }

    public async Task<CustomCardOTD> SetCustomCardOfTheDay(Guid cardId, Guid? accountId)
    {
        CustomCard? card = await ctx.CustomCards.FindAsync(cardId);
        if (card is null) throw new KeyNotFoundException("That card was not found!");

        CustomCardOTD ccOTD;

        if (accountId is null) 
            ccOTD = new CustomCardOTD(card, true);
        else {
            UserAccount? account = await ctx.UserAccounts.FindAsync(accountId);
            if (account is null) throw new KeyNotFoundException("That account was not found!");
            if ((account.Permissions & Enums.UserPermissions.SetCustomCardOfTheDay) == 0) throw new NotAuthorizedException("You do not have permissions for this!");
            ccOTD = new CustomCardOTD(card, false, account);
        }

        await ctx.CustomCardsOTD.AddAsync(ccOTD);
        await SaveAsync();
        return ccOTD;
    }

    public async Task<CustomCard> ValidateCustomCard(Guid cardId, Guid accountId, Enums.CustomCardApprovalState state)
    {
        CustomCard? card = await ctx.CustomCards.FindAsync(cardId);
        if (card is null) throw new KeyNotFoundException("That card was not found!");
        
        UserAccount? account = await ctx.UserAccounts.FindAsync(accountId);
        if (account is null) throw new KeyNotFoundException("That account was not found!");
        if ((account.Permissions & Enums.UserPermissions.ValidateCustomCard) == 0) throw new NotAuthorizedException("You do not have permissions for this!");

        card.State = state;
        await SaveAsync();
        return card;
    }
}