using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories.DAL;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {     }

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<CustomCard> CustomCards { get; set; }
    public DbSet<CustomCardOTD> CustomCardsOTD { get; set; }
    // public DbSet<Card> Cards { get; set; }
    public DbSet<Deck> Decks { get; set; }
    public DbSet<DeckOTD> DecksOTD { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserAccount>().HasMany<CustomCard>(x => x.OwnedCustomCards).WithOne(x => x.OwnerAccount);
        builder.Entity<UserAccount>().HasMany<CustomCard>(x => x.LikedCustomCards).WithMany(x => x.LikedUsers);

        builder.Entity<UserAccount>().HasMany<Deck>(x => x.OwnedDecks).WithOne(x => x.OwnerAccount);
        builder.Entity<UserAccount>().HasMany<Deck>(x => x.LikedDecks).WithMany(x => x.LikedUsers);

        builder.Entity<UserAccount>().HasIndex(a => a.Username).IsUnique();

        builder.Entity<CustomCardOTD>().HasOne(x => x.CardSetter).WithMany();
        builder.Entity<DeckOTD>().HasOne(x => x.DeckSetter).WithMany();

        builder.Entity<CustomCard>().Navigation(x => x.OwnerAccount).AutoInclude();
        builder.Entity<CustomCardOTD>().Navigation(x => x.Card).AutoInclude();
        builder.Entity<CustomCardOTD>().Navigation(x => x.CardSetter).AutoInclude();
        builder.Entity<Deck>().Navigation(x => x.OwnerAccount).AutoInclude();
        builder.Entity<DeckOTD>().Navigation(x => x.Deck).AutoInclude();
        builder.Entity<DeckOTD>().Navigation(x => x.DeckSetter).AutoInclude();
    }
}