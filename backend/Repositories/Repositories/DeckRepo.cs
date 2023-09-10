using Repositories.DAL;
using Repositories.Models;
using LorDeckEncoder = Kunc.RiotGames.Lor.DeckCodes.LorDeckEncoder;
using KuncDeckItem = Kunc.RiotGames.Lor.DeckCodes.DeckItem;
using Repositories.Interfaces;
using Repositories.Utility;

namespace Repositories.Repositories;

public class DeckRepo : Repository<Deck>, IDeckRepo
{
    public DeckRepo(DataContext _ctx) : base(_ctx) { }

    public override async Task<Deck> GetById(Guid id)
    {
        Deck deck = await base.GetById(id);
        deck.DeckContent = GetCardsFromCode(deck.DeckCode);
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

    private ICollection<DeckItem> GetCardsFromCode(string deckCode)
    {
        var deckItems = new LorDeckEncoder().GetDeckFromCode<KuncDeckItem>(deckCode);
        ICollection<DeckItem> deck = deckItems.Select(x => new DeckItem() { Count = x.Count, Card = Utility.Utils.Cards.First(c => c.CardCode == x.CardCode) }).ToList();
        return deck;
    }
}