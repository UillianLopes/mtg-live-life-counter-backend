using IdentityModel;
using MtgLiveLifeCounter.Core.Extensions;
using System.Security.Claims;

namespace MtgLiveLifeCounter.Domain.Entities
{
    public class User : Entity
    {
        protected User() : base()
        {
            Rooms = new HashSet<Room>();
            ConnectedRooms = new HashSet<RoomUser>();
        }

        public User(string username, string password, string email) : this()
        {
            Username = username;
            Password = password.HashPassword();
            Email = email;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public virtual ISet<Room> Rooms { get; private set; }
        public virtual ISet<RoomUser> ConnectedRooms { get; private set; } 

        public IEnumerable<Claim> Claims()
        {
            yield return new Claim(JwtClaimTypes.Name, Username);
            yield return new Claim(JwtClaimTypes.Email, Email);
            yield return new Claim(JwtClaimTypes.Id, Id.ToString());
        }
    }
}
