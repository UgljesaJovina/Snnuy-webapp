using Repositories.Models;

namespace Repositories.Interfaces;

public interface IUserRepo : IRepository<UserAccount>
{
    public Task<UserAccount> Authenticate(string username, string password);
}