using Repositories.DAL;
using Repositories.Models;
using Repositories.Interfaces;
using Repositories.Utility;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class DeckRepo : Repository<Deck>, IDeckRepo
{
    public DeckRepo(DataContext _ctx) : base(_ctx) { }

    public override async Task<Deck> GetById(Guid id)
    {
        Deck? deck = await table.Include(x => x.LikedUsers).FirstOrDefaultAsync(x => x.Id == id);

        if (deck is null) throw new KeyNotFoundException();

        return deck;
    }

    public async Task<DeckOTD> SetDeckOfTheDay(Guid deckId, UserAccount? account = null)
    {
        Deck? deck = await table.FindAsync(deckId);
        if (deck is null) throw new KeyNotFoundException("That card was not found!");

        DeckOTD dOTD;

        if (account is null) 
            dOTD = new DeckOTD(deck, true);
        else {
            if ((account.Permissions & Enums.UserPermissions.SetCustomCardOfTheDay) == 0) throw new NotAuthorizedException("You do not have permissions for this!");
            dOTD = new DeckOTD(deck, false, account);
        }

        await ctx.DecksOTD.AddAsync(dOTD);
        await SaveAsync();
        Utils.LAST_DECKOTD_SET = DateTime.Now;
        return dOTD;
    }

    public async Task<DeckOTD> GetLastDeckOTD()
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

        deck.LikedUsers.Add(account);
        await SaveAsync();
    }
}