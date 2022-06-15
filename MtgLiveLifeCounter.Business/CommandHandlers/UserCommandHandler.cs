using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MtgLiveLifeCounter.Core;
using MtgLiveLifeCounter.Core.Configurations;
using MtgLiveLifeCounter.Core.Extensions;
using MtgLiveLifeCounter.Domain.Commands.Users;
using MtgLiveLifeCounter.Domain.Contracts.Repositories;
using MtgLiveLifeCounter.Domain.Entities;
using MtgLiveLifeCounter.Domain.Queries.Outputs;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MtgLiveLifeCounter.Business.CommandHandlers
{
    public class UserCommandHandler : ICommandHandler<CreateUserCommand>, ICommandHandler<AuthenticateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public UserCommandHandler(IUserRepository userRepository, IOptions<AuthenticationConfiguration> authenticationConfiguration)
        {
            _userRepository = userRepository;
            _authenticationConfiguration = authenticationConfiguration.Value;
        }

        public async Task<ICommandOuput> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByUserAsync(request.Username, cancellationToken) is User)
                return CommandOutput.BadRequest("Username already exists");

            var user = new User(request.Username, request.Password, request.Email);

            await _userRepository.AddAsync(user, cancellationToken);

            return CommandOutput.Ok("User created with success");
        }

        public async Task<ICommandOuput> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByUserAsync(request.Username, cancellationToken) is not User user)
                return CommandOutput.NotFound("Invalid user name or password");

            if (user.Password != request.Password.HashPassword())
                return CommandOutput.BadRequest("Invalid user name or password");
            
            var issuer = _authenticationConfiguration.ValidIssuer;
            var audience = _authenticationConfiguration.ValidAudience;
            var expires = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationConfiguration.IssuerSigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer, 
                audience: audience, 
                expires: expires, 
                signingCredentials: credentials,
                claims: user.Claims()
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return CommandOutput.Ok(new
            {
                token = stringToken,
                expires = expires,
                user = new UserQueryOutput 
                { 
                    Username = user.Username,
                    Email = user.Email,
                }
            });
        }
    }
}
