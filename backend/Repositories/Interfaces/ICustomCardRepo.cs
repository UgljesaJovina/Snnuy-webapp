using Repositories.Models;

namespace Repositories.Interfaces;

public interface ICustomCardRepo : IRepository<CustomCard>
{
    // public Task<CustomCard?> SaveCard(Stream stream);
    // nalazi se u customCardRepo-u, ali je private
    public Task<CustomCardOTD> SetCustomCardOfTheDay(Guid cardId, UserAccount? account);
    public Task<CustomCard> ValidateCustomCard(Guid cardId, UserAccount account, Enums.CustomCardApprovalState state);
}