using Microsoft.EntityFrameworkCore;
using Repositories.DAL;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories.Repositories;

public class UserRepo : Repository<UserAccount>, IUserRepo
{
    public UserRepo(DataContext _ctx) : base(_ctx) { }

    public async Task<UserAccount> Authenticate(string username, string password)
    {
        UserAccount? account = await table.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

        if (account is null) throw new KeyNotFoundException("That pair of username and password doesn't exist");

        return account;
    }
}