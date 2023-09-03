using Repositories.Models;

namespace Repositories.Interfaces;

public interface ICustomCardRepo : IRepository<CustomCard>
{
    // public Task<CustomCard?> SaveCard(Stream stream);
    // nalazi se u customCardRepo-u, ali je private
    public Task<CustomCardOTD> SetCustomCardOfTheDay(Guid cardId, Guid? accountId);
    public Task<CustomCard> ValidateCustomCard(Guid cardId, Guid accountId, Enums.CustomCardApprovalState state);
}