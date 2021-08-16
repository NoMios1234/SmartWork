using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartWork.Core.Entities;

namespace SmartWork.Data.MappingConfigs
{
    public class SubscribeDetailsMappingConfig : IEntityTypeConfiguration<SubscribeDetail>
    {
        public void Configure(EntityTypeBuilder<SubscribeDetail> builder)
        {
            builder.ToTable("SubscribeDetails");

            builder.Property(x => x.SubscribeName)
                .HasMaxLength(128)
                .IsRequired(true);
            builder.Property(x => x.SubscribeDescription)
                .HasMaxLength(128)
                .IsRequired(false);
            builder.Property(x => x.SubscribePrice)
                .IsRequired(true);
            builder.Property(x => x.OfficeId)
                .IsRequired(true);
        }
    }
}
