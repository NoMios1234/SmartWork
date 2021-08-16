using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartWork.Core.Entities;

namespace SmartWork.Data.MappingConfigs
{
    public class OfficeMappingConfig : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable("Offices");

            builder.Property(x => x.OfficeName)
                .HasMaxLength(128)
                .IsRequired(true);
            builder.Property(x => x.OfficeAddress)
                .HasMaxLength(128)
                .IsRequired(true);
            builder.Property(x => x.OfficePhoneNumber)
                .HasMaxLength(12)
                .IsRequired(true);
            builder.Property(x => x.PhotoFileName)
                .HasMaxLength(128)
                .IsRequired(true);            
            builder.Property(x => x.CompanyId)
                .IsRequired(true);
        }
    }
}
