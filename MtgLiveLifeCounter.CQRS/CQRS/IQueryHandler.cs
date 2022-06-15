using MediatR;

namespace MtgLiveLifeCounter.Core
{
    public interface IQueryHandler<T> : IRequestHandler<T, IQueryOutput> where T : IQuery
    {
    }
}
