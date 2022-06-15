using MtgLiveLifeCounter.Core;

namespace MtgLiveLifeCounter.Domain.Commands.Users
{
    public class CreateUserCommand : ICommand
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
