using MtgLiveLifeCounter.Core.Contracts;
using MtgLiveLifeCounter.Infra.Connections;

namespace MtgLiveLifeCounter.Infra.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly MtgLiveLifeCounterDbContext _dbContext;

        public Uow(MtgLiveLifeCounterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);

                //foreach (var entry in _dbContext.ChangeTracker.Entries())
                //{
                //    if (entry.Entity is not Entity entity)
                //        continue;


                //    foreach (var command in entity.Events)
                //        try
                //        {
                //            await _mediator.Publish(command, cancellationToken);
                //        }
                //        catch (Exception ex)
                //        {
                //            continue;
                //        }


                //    entity.ClearEvents();
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
