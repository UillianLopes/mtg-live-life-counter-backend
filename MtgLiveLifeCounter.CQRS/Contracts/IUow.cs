namespace MtgLiveLifeCounter.Core.Contracts
{
    public interface IUow
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
