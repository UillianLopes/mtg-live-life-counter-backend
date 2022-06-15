using MtgLiveLifeCounter.Domain.Entities;

namespace MtgLiveLifeCounter.Domain.Contracts.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);

        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
