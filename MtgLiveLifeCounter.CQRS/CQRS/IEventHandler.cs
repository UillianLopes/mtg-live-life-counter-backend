using MediatR;

namespace MtgLiveLifeCounter.Core
{
    public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent
    {
    }
}
