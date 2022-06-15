using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MtgLiveLifeCounter.Domain.Entities;

namespace MtgLiveLifeCounter.Infra.Mappings
{
    public class RoomMap : EntityMap<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(b => b.Password)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(b => b.Life)
                .IsRequired();

            builder.HasMany(e => e.ConnectedUsers)
                .WithOne(e => e.Room)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.User)
                .WithMany(b => b.Rooms)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
