using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories.DAL;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<CustomCard> CustomCards { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Deck> Decks { get; set; }
    
}