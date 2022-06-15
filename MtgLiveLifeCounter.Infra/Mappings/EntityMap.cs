using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MtgLiveLifeCounter.Domain.Entities;

namespace MtgLiveLifeCounter.Infra.Mappings
{
    public abstract class EntityMap<T> : IEntityTypeConfiguration<T> where T : Entity

    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Creation)
                .IsRequired();

            builder.Property(e => e.Update);
        }
    }
}
