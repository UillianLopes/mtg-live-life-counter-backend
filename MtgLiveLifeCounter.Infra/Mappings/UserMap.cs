using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MtgLiveLifeCounter.Domain.Entities;

namespace MtgLiveLifeCounter.Infra.Mappings
{
    public class UserMap : EntityMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Username)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(200);

            builder.HasMany(e => e.Rooms)
                .WithOne(e => e.User)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.ConnectedRooms)
                .WithOne(e => e.User)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
