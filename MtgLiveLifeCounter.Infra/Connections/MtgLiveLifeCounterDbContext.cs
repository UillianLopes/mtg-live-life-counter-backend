using Microsoft.EntityFrameworkCore;
using MtgLiveLifeCounter.Domain.Entities;
using MtgLiveLifeCounter.Infra.Mappings;

namespace MtgLiveLifeCounter.Infra.Connections
{
    public class MtgLiveLifeCounterDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomUser> RoomUsers { get; set; }
        public DbSet<User> Users { get; set; }

        public MtgLiveLifeCounterDbContext(DbContextOptions options): base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoomMap());
            modelBuilder.ApplyConfiguration(new RoomUserMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
