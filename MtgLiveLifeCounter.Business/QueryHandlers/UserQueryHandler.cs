using MtgLiveLifeCounter.Core;
using MtgLiveLifeCounter.Core.Contracts;
using MtgLiveLifeCounter.Domain.Queries.Inputs;

namespace MtgLiveLifeCounter.Business.QueryHandlers
{
    public class UserQueryHandler : IQueryHandler<GetUserQuery>
    {
        private readonly IAuthenticatedUser _authenticatedUser;

        public UserQueryHandler(IAuthenticatedUser authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        public async Task<IQueryOutput> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return QueryOutput.Ok(new
            {
                Name = _authenticatedUser.Email,
                Username = _authenticatedUser.Username
            });
        }
    }
}
