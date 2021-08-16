using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartWork.Core.Entities;

namespace SmartWork.Data.MappingConfigs
{
    public class SubscribeMappingConfig : IEntityTypeConfiguration<Subscribe>
    {
        public void Configure(EntityTypeBuilder<Subscribe> builder)
        {
            builder.ToTable("Subscribes");

            builder.Property(x => x.SubscribeName)
                .HasMaxLength(128)
                .IsRequired(true);
            builder.Property(x => x.SubscribeDescription)
                .HasMaxLength(128)
                .IsRequired(false);
            builder.Property(x => x.StartSubscribe)
                .IsRequired(true);
            builder.Property(x => x.EndSubscribe)
                .IsRequired(true);
            builder.Property(x => x.UserId)
                .IsRequired(true);
            builder.Property(x => x.SubscribeDetailId)
                .IsRequired(true);
        }
    }
}
