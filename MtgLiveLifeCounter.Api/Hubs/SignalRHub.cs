using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MtgLiveLifeCounter.Api.Hubs
{
    [Authorize]
    public class SignalRHub : Hub
    {
        public async Task AddToGroupAsync(string groupName, CancellationToken cancellationToken) => await Groups
            .AddToGroupAsync(Context.ConnectionId, groupName, cancellationToken);
    }
}
