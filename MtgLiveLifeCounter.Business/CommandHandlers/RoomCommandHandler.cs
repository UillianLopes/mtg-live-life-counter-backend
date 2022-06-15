using MtgLiveLifeCounter.Core;
using MtgLiveLifeCounter.Core.Contracts;
using MtgLiveLifeCounter.Domain.Commands.Rooms;
using MtgLiveLifeCounter.Domain.Contracts.Repositories;
using MtgLiveLifeCounter.Domain.Entities;

namespace MtgLiveLifeCounter.Business.CommandHandlers
{
    public class RoomCommandHandler : ICommandHandler<CreateRoomCommand>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticatedUser _authenticatedUser;

        public RoomCommandHandler(IRoomRepository roomRepository, IUserRepository userRepository, IAuthenticatedUser authenticatedUser)
        {
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<ICommandOuput> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdAsync(_authenticatedUser.Id, cancellationToken) is not User user)
                return CommandOutput.NotFound("");

            var room = new Room(request.Name, request.Password, request.Life, user);

            await _roomRepository.AddAsync(room, cancellationToken);

            return CommandOutput.Ok("");
        }
    }
}
