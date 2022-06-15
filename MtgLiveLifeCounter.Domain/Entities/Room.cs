namespace MtgLiveLifeCounter.Domain.Entities
{
    public class Room : Entity
    {
        protected Room() : base() 
        {
            ConnectedUsers = new HashSet<RoomUser>();
        }

        public Room(string name, string password, int life, User user) : this()
        {
            Name = name;
            Password = password;
            Life = life;
            User = user;
        }

        public string Name { get; private set; }
        public string Password { get; private set; }
        public int Life { get; private set; }
        public virtual User User { get; private set; }
        public virtual ISet<RoomUser> ConnectedUsers { get; private set; }
    }
}
