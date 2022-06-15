using MtgLiveLifeCounter.Domain.Contracts.Repositories;
using MtgLiveLifeCounter.Domain.Entities;
using MtgLiveLifeCounter.Infra.Connections;

namespace MtgLiveLifeCounter.Infra.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(MtgLiveLifeCounterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
