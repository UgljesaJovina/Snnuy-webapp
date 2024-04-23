using System.Timers;
using Microsoft.EntityFrameworkCore;
using Repositories.DAL;
using Repositories.Filters;
using Repositories.Interfaces;
using Repositories.Models;
using Repositories.Utility;
using Repositories.Enums;

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
            .FirstOrDefaultAsync(x => x.Id == id);

        if (card is null) throw new KeyNotFoundException("The card was not found in the database");

        return card;
    }

    public async override Task<ICollection<CustomCard>> GetAll() {
        return await table.Include(x => x.LikedUsers).AsQueryable().Where(c => c.ApprovalState == CustomCardApprovalState.Approved).ToListAsync();
    }

    public async Task<ICollection<CustomCard>> GetAll(CardFilter filter) {
        IEnumerable<CustomCard> cards = table.Include(x => x.LikedUsers).AsQueryable();

        cards = cards.Where(c => c.ApprovalState == filter.ApprovalState);
        
        if (filter.PostedAfter is not null) cards = cards.Where(c => c.PostingDate > filter.PostedAfter);
        if (filter.PostedBefore is not null) cards = cards.Where(c => c.PostingDate < filter.PostedBefore);

        cards = cards.Where(c => (c.Regions & filter.Regions) != 0);
        cards = cards.Where(c => (c.Type & filter.Type) != 0);

        if (filter.ByPopularity == SortByPopularity.None) {
            if (filter.ByDate == SortByDate.Newest) cards = cards.OrderByDescending(c => c.PostingDate);
            else cards = cards.OrderBy(c => c.PostingDate);
        }   
        else if (filter.ByPopularity == SortByPopularity.MostPopular) cards = cards.OrderByDescending(c => c.NumberOfLikes);
        else if (filter.ByPopularity == SortByPopularity.LeastPopular) cards = cards.OrderBy(c => c.NumberOfLikes);

        return cards.Skip(filter.Skip).Take(filter.Take).ToList();
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
            if ((account.Permissions & UserPermissions.SetCustomCardOfTheDay) == 0) throw new NotAuthorizedException("You do not have permissions for this!");
            ccOTD = new CustomCardOTD(card, account);
        }

        await ctx.CustomCardsOTD.AddAsync(ccOTD);
        await SaveAsync();
        Utils.LAST_CARDOTD_SET = DateTime.Now;
        return ccOTD;
    }

    public async Task<CustomCard> ValidateCustomCard(Guid cardId, UserAccount account, bool approvalState)
    {
        CustomCard? card = await table.FindAsync(cardId);
        if (card is null) throw new KeyNotFoundException("That card was not found!");
        
        card.ApprovalState = approvalState ? CustomCardApprovalState.Approved : CustomCardApprovalState.Cancelled;
        await SaveAsync();
        return card;
    }

    public async Task<ICollection<CustomCardOTD>> GetAllCustomCardsOTD()
    {
        return await ctx.CustomCardsOTD.Include(x => x.Card.LikedUsers).ToListAsync();
    }

    public async Task<CustomCardOTD> GetLatestCustomCardOTD()
    {
        var card = await ctx.CustomCardsOTD.FirstOrDefaultAsync();
        if (card is null) throw new KeyNotFoundException("There are no custom cards of the day");
        return card;
    }

    public async Task<CardLikeRecord> LikeACard(Guid id, UserAccount account) {
        CustomCard? card = await table.Include(x => x.LikedUsers).FirstOrDefaultAsync(x => x.Id == id);
        if (card is null) throw new KeyNotFoundException("Card was not found");
        
        CardLikeRecord ret;

        if (card.LikedUsers.Contains(account)) { card.LikedUsers.Remove(account); ret = new(false, card.NumberOfLikes); }
        else { card.LikedUsers.Add(account); ret = new(true, card.NumberOfLikes); }
        
        await SaveAsync();
        return ret;
    }

    private async void AutomaticCardSet(object? sender, ElapsedEventArgs e)
    {
        if (DateTime.Now - Utils.LAST_CARDOTD_SET >= Utils.AUTOMATIC_CARDOTD_DELAY) {
            var cardOTDIds = ctx.CustomCardsOTD.Select(cotd => cotd.Card.Id);
            IEnumerable<CustomCard> cards = (await GetAll()).Where(c => !cardOTDIds.Contains(c.Id));

            if (cards.Count() == 0) return;

            CustomCard newCardOTD = cards.OrderBy(x => x.NumberOfLikes).First();
            await SetCustomCardOTD(newCardOTD.Id, card: newCardOTD);
        }
    }
}
