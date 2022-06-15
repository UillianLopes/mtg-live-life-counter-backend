using IdentityModel;
using MtgLiveLifeCounter.Core.Contracts;
using System.Security.Claims;
using System.Security.Principal;

namespace MtgLiveLifeCounter.Api.Authentication
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly ClaimsPrincipal _principal;

        public AuthenticatedUser(IPrincipal principal) => _principal = (ClaimsPrincipal)principal;

        public Guid Id => Guid.TryParse(_principal.Claims.FirstOrDefault(e => e.Type == JwtClaimTypes.Id)?.Value, out Guid id) ? id : Guid.Empty;

        public string? Email => _principal.Claims.FirstOrDefault(e => e.Type == JwtClaimTypes.Email)?.Value;

        public string? Username => _principal.Claims.FirstOrDefault(e => e.Type == JwtClaimTypes.Name)?.Value;
    }
}
