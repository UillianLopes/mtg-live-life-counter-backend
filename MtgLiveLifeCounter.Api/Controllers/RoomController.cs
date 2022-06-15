using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MtgLiveLifeCounter.Api.Abstracts;
using MtgLiveLifeCounter.Core.Contracts;
using MtgLiveLifeCounter.Domain.Commands.Rooms;

namespace MtgLiveLifeCounter.Api.Controllers
{
    [Route("[controller]")]
    public class RoomController : BaseController
    {
        public RoomController(IMediator mediator, IUow uow) : base(mediator, uow)
        {
        }

        [HttpPost]
        [Authorize]
        public Task CreateAsync(CreateRoomCommand command, CancellationToken cancellationToken) => SendAsync(command, cancellationToken);
    }
}
