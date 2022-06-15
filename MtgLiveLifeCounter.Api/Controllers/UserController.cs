using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MtgLiveLifeCounter.Api.Abstracts;
using MtgLiveLifeCounter.Core.Contracts;
using MtgLiveLifeCounter.Domain.Commands.Users;
using MtgLiveLifeCounter.Domain.Queries.Inputs;

namespace MtgLiveLifeCounter.Api.Controllers
{

    [Route("[controller]")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator, IUow uow) : base(mediator, uow)
        {
        }


        [HttpGet]
        [Authorize]
        public Task<IActionResult> GetUserAsync(CancellationToken cancellationToken) => QueryAsync(new GetUserQuery(), cancellationToken);

        [HttpPost]
        public Task<IActionResult> CreateAsync([FromBody] CreateUserCommand command, CancellationToken cancellationToken) => SendAsync(command, cancellationToken);

        [HttpPost("[action]")]
        public Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateCommand command, CancellationToken cancellationToken) => SendAsync(command, cancellationToken);
    }
}
