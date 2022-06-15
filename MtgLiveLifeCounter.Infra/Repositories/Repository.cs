using Microsoft.EntityFrameworkCore;
using MtgLiveLifeCounter.Domain.Contracts.Repositories;
using MtgLiveLifeCounter.Domain.Entities;
using MtgLiveLifeCounter.Infra.Connections;

namespace MtgLiveLifeCounter.Infra.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly MtgLiveLifeCounterDbContext _dbContext;

        public Repository(MtgLiveLifeCounterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken) => await _dbContext
            .Set<T>()
            .AddAsync(entity, cancellationToken);

        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => _dbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }
}
