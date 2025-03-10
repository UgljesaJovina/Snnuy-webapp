using Microsoft.EntityFrameworkCore;
using Repositories.DAL;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories.Repositories;

public class UserRepo : Repository<UserAccount>, IUserRepo
{
    public UserRepo(DataContext _ctx) : base(_ctx) { }

    public async override Task<UserAccount> GetById(Guid id)
    {
        UserAccount? instance = await table
            .Include(x => x.OwnedCustomCards)
            .Include(x => x.OwnedDecks)
            .Include(x => x.LikedCustomCards)
            .Include(x => x.LikedDecks)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (instance is null) throw new KeyNotFoundException($"User does not exist!");

        return instance;
    }

    public async Task<UserAccount> GetByUsername(string username)
    {
        UserAccount? account = await table
            .Include(x => x.LikedCustomCards)
            .Include(x => x.LikedDecks)
            .FirstOrDefaultAsync(x => x.Username == username);

        if (account is null/* || account.Username != username*/) throw new KeyNotFoundException("That username does not exist");

        return account;
    }

    public override async Task<UserAccount> Create(UserAccount account) {
        if (table.FirstOrDefault(u => u.Username == account.Username) is not null) throw new ArgumentException("An account with that username already exists!");
        return await base.Create(account);
    }

    public override async Task<UserAccount> Update(Guid id, UserAccount updatedAccount) {
        UserAccount? account = await table.FindAsync(id);
        if (account is null) throw new KeyNotFoundException("That account was not found");

        if (!string.IsNullOrEmpty(updatedAccount.Username)) account.Username = updatedAccount.Username;
        if (!string.IsNullOrEmpty(updatedAccount.HashedPassword)) account.HashedPassword = updatedAccount.HashedPassword;
        
        await SaveAsync();

        return account;
    }
}