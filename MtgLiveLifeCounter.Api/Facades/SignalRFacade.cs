using Microsoft.AspNetCore.SignalR;
using MtgLiveLifeCounter.Api.Hubs;
using MtgLiveLifeCounter.Core.Contracts;

namespace MtgLiveLifeCounter.Api.Facades
{
    public class SignalRFacade : ISignalRFacade
    {
        private readonly SignalRHub _signalRHub;
        private readonly IAuthenticatedUser _authenticatedUser;

        public SignalRFacade(SignalRHub signalRHub, IAuthenticatedUser authenticatedUser)
        {
            _signalRHub = signalRHub;
            _authenticatedUser = authenticatedUser;
        }

        public async Task SendToAllAsync<T>(string method, T message, CancellationToken cancellationToken) => await _signalRHub.Clients.All
            .SendAsync(method, message, cancellationToken);

        public async Task SendToGroupAsync<T>(string method, string group, T message, CancellationToken cancellationToken) => await _signalRHub.Clients
            .Group(group).SendAsync(method, message, cancellationToken);

        public async Task SendToUserAsync<T>(string method, T message, CancellationToken cancellationToken) => await _signalRHub.Clients
            .User(_authenticatedUser.Id.ToString())
            .SendAsync(method, message, cancellationToken);

    }
}
