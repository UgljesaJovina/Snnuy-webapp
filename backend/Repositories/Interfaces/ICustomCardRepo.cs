using Repositories.Filters;
using Repositories.Models;

namespace Repositories.Interfaces;

public interface ICustomCardRepo : IRepository<CustomCard>
{
    // public Task<CustomCard?> SaveCard(Stream stream);
    // nalazi se u customCardRepo-u, ali je private
    public Task<ICollection<CustomCard>> GetAll (CardFilter filter);
    public Task<CustomCardOTD> SetCustomCardOTD(Guid cardId, UserAccount? account = null, CustomCard? card = null);
    public Task<ICollection<CustomCardOTD>> GetAllCustomCardsOTD();
    public Task<CustomCardOTD> GetLatestCustomCardOTD();
    public Task<CustomCard> ValidateCustomCard(Guid cardId, UserAccount account, bool approvalState);
    public Task<CardLikeRecord> LikeACard(Guid id, UserAccount account);
}