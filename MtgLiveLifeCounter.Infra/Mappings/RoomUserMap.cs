using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MtgLiveLifeCounter.Domain.Entities;

namespace MtgLiveLifeCounter.Infra.Mappings
{
    public class RoomUserMap : EntityMap<RoomUser>
    {
        public override void Configure(EntityTypeBuilder<RoomUser> builder)
        {
            base.Configure(builder);

            builder.HasOne(e => e.User)
                .WithMany(e => e.ConnectedRooms)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Room)
                .WithMany(e => e.ConnectedUsers)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Life)
                .IsRequired();
        }
    }
}
