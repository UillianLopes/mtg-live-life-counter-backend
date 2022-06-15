using MediatR;

namespace MtgLiveLifeCounter.Core
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ICommandOuput> where TCommand : ICommand
    {
    }
}
