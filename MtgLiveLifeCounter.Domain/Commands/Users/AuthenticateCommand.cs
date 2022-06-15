using MtgLiveLifeCounter.Core;

namespace MtgLiveLifeCounter.Domain.Commands.Users
{
    public class AuthenticateCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
