namespace MtgLiveLifeCounter.Domain.Entities
{
    public class RoomUser : Entity
    {
        protected RoomUser() : base() { }

        public RoomUser(int life, User user, Room room) : this()
        {
            Life = life;
            User = user;
            Room = room;
        }

        public int Life { get; private set; }
        public virtual User User { get; private set; }
        public virtual Room Room { get; private set; }
    }
}
