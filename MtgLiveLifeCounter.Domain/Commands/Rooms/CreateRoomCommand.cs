using MtgLiveLifeCounter.Core;

namespace MtgLiveLifeCounter.Domain.Commands.Rooms
{
    public class CreateRoomCommand : ICommand
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public int Life { get; set; }
    }
}
