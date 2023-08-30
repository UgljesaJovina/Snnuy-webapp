using Microsoft.EntityFrameworkCore;

namespace Repositories.DAL;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    
}