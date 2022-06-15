using MtgLiveLifeCounter.Domain.Entities;

namespace MtgLiveLifeCounter.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUserAsync(string username, CancellationToken cancellationToken);
    }
}
