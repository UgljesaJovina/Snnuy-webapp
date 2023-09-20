using Repositories.DAL;
using Repositories.Models;
using Repositories.Interfaces;
using Repositories.Utility;
using Microsoft.EntityFrameworkCore;
using System.Timers;

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
        return await ctx.DecksOTD.FirstAsync();
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
        if (DateTime.Now - Utils.LAST_CARDOTD_SET >= Utils.AUTOMATIC_CARDOTD_DELAY) {
            IEnumerable<Deck> decks = (await GetAll()).Where(d => !ctx.DecksOTD.Select(dotd => dotd.Id).Contains(d.Id));

            if (decks.Count() == 0) return;

            Deck newDeckOTD = decks.OrderBy(x => x.NumberOfLikes).First();
            await SetDeckOfTheDay(newDeckOTD.Id, deck: newDeckOTD);
            Utils.LAST_DECKOTD_SET = DateTime.Now;
        }
    }
}