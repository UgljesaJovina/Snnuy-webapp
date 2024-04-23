using Repositories.DAL;
using Repositories.Models;
using Repositories.Interfaces;
using Repositories.Utility;
using Microsoft.EntityFrameworkCore;
using System.Timers;
using Repositories.Filters;
using Repositories.Enums;

namespace Repositories.Repositories;

public class DeckRepo : Repository<Deck>, IDeckRepo
{
    static bool subscribed = false;

    public DeckRepo(DataContext _ctx) : base(_ctx) { 
        if (!subscribed) {
            Utils.DeckTimer.Elapsed += AutomaticDeckSet;
            subscribed = true;
        }
    }

    public override async Task<Deck> GetById(Guid id)
    {
        Deck? deck = await table.FindAsync(id);

        if (deck is null) throw new KeyNotFoundException();

        return deck;
    }

    public async Task<ICollection<Deck>> GetAll(DeckFilter filter) {
        IEnumerable<Deck> decks = table.Include(x => x.LikedUsers).AsQueryable();

        if (filter.PostedAfter is not null) decks = decks.Where(d => d.PostingDate > filter.PostedAfter);
        if (filter.PostedBefore is not null) decks = decks.Where(d => d.PostingDate < filter.PostedBefore);

        decks = decks.Where(d => (d.DeckRegions & filter.Regions) != 0);
        decks = decks.Where(d => (d.Type & filter.Types) != 0);

        if (!filter.IncludeEternal) decks = decks.Where(d => d.Standard);

        if (filter.ByPopularity == SortByPopularity.None)
        {
            if (filter.ByDate == SortByDate.Newest) decks = decks.OrderByDescending(c => c.PostingDate);
            else decks = decks.OrderBy(c => c.PostingDate);
        }
        else if (filter.ByPopularity == SortByPopularity.MostPopular) decks = decks.OrderByDescending(c => c.NumberOfLikes);
        else if (filter.ByPopularity == SortByPopularity.LeastPopular) decks = decks.OrderBy(c => c.NumberOfLikes);

        return decks.Skip(filter.Skip).Take(filter.Take).ToList();
    }

    public async Task<DeckOTD> SetDeckOfTheDay(Guid deckId, UserAccount? account = null, Deck? deck = null)
    {
        deck ??= await table.FindAsync(deckId);
        if (deck is null) throw new KeyNotFoundException("That card was not found!");

        DeckOTD dOTD;

        if (account is null) 
            dOTD = new DeckOTD(deck);
        else {
            if ((account.Permissions & Enums.UserPermissions.SetCustomCardOfTheDay) == 0) throw new NotAuthorizedException("You do not have permissions for this!");
            dOTD = new DeckOTD(deck, account);
        }

        await ctx.DecksOTD.AddAsync(dOTD);
        await SaveAsync();
        Utils.LAST_DECKOTD_SET = DateTime.Now;
        return dOTD;
    }

    public async Task<DeckOTD> GetLatestDeckOTD()
    {
        var deck = await ctx.DecksOTD.FirstOrDefaultAsync();
        if (deck is null) throw new KeyNotFoundException("There are no decks of the day");
        return deck;
    }

    public async Task<ICollection<DeckOTD>> GetAllDecksOTD()
    {
        return await ctx.DecksOTD.ToListAsync();
    }

    public async Task LikeADeck(Guid id, UserAccount account) {
        Deck? deck = await table.FindAsync(id);
        if (deck is null) throw new KeyNotFoundException("The deck was not found");

        if (deck.LikedUsers.Contains(account)) { deck.LikedUsers.Remove(account); deck.NumberOfLikes -= 1; }
        else { deck.LikedUsers.Add(account); deck.NumberOfLikes += 1; }

        await SaveAsync();
    }

    private async void AutomaticDeckSet(object? sender, ElapsedEventArgs e)
    {
        if (DateTime.Now - Utils.LAST_DECKOTD_SET >= Utils.AUTOMATIC_DECKOTD_DELAY) {
            var deckOTDIds = ctx.DecksOTD.Select(dotd => dotd.Deck.Id);
            IEnumerable<Deck> decks = (await GetAll()).Where(d => !deckOTDIds.Contains(d.Id));

            if (decks.Count() == 0) return;

            Deck newDeckOTD = decks.OrderBy(x => x.NumberOfLikes).First();
            await SetDeckOfTheDay(newDeckOTD.Id, deck: newDeckOTD);
        }
    }
}