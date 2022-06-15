namespace MtgLiveLifeCounter.Core.Contracts
{
    public interface ISignalRFacade
    {
        Task SendToAllAsync<T>(string method, T message, CancellationToken cancellationToken);

        Task SendToGroupAsync<T>(string method, string group, T message, CancellationToken cancellationToken);

        Task SendToUserAsync<T>(string method, T message, CancellationToken cancellationToken);
    }
}
