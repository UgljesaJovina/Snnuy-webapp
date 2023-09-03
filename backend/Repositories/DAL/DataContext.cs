using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories.DAL;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<CustomCard> CustomCards { get; set; }
    public DbSet<CustomCardOTD> CustomCardsOTD { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Deck> Decks { get; set; }
    public DbSet<DeckOTD> DecksOTD { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>().HasMany<CustomCard>(x => x.OwnedCustomCards).WithOne(x => x.OwnerAccount);
        modelBuilder.Entity<UserAccount>().HasMany<CustomCard>(x => x.LikedCustomCards).WithMany(x => x.LikedUsers);

        modelBuilder.Entity<UserAccount>().HasMany<Deck>(x => x.OwnedDecks).WithOne(x => x.OwnerAccount);
        modelBuilder.Entity<UserAccount>().HasMany<Deck>(x => x.LikedDecks).WithMany(x => x.LikedUsers);
    }
}