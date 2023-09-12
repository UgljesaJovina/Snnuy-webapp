using Repositories.Models;

namespace Repositories.Interfaces;

public interface IUserRepo : IRepository<UserAccount>
{
    public Task<UserAccount> GetByUsername(string username);
}