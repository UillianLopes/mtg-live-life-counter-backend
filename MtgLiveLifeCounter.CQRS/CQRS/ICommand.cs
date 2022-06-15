using MediatR;

namespace MtgLiveLifeCounter.Core
{
    public interface ICommand : IRequest<ICommandOuput>
    {
    }
}
