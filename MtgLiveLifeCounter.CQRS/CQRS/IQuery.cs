using MediatR;

namespace MtgLiveLifeCounter.Core
{
    public interface IQuery : IRequest<IQueryOutput>
    {
    }
}
