using Microsoft.EntityFrameworkCore;
using MtgLiveLifeCounter.Domain.Contracts.Repositories;
using MtgLiveLifeCounter.Domain.Entities;
using MtgLiveLifeCounter.Infra.Connections;

namespace MtgLiveLifeCounter.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MtgLiveLifeCounterDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetByUserAsync(string username, CancellationToken cancellationToken) => await _dbContext
            .Set<User>()
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }
}
