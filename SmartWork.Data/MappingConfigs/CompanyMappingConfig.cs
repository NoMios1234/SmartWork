using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartWork.Core.Entities;

namespace SmartWork.Data.MappingConfigs
{
    public class CompanyMappingConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.Property(x => x.CompanyName)
                .HasMaxLength(128)
                .IsRequired(true);
            builder.Property(x => x.CompanyAddress)
                .HasMaxLength(128)
                .IsRequired(true);
            builder.Property(x => x.CompanyDescription)
                .HasMaxLength(128)
                .IsRequired(false);
            builder.Property(x => x.CompanyPhoneNumber)
                .HasMaxLength(12)
                .IsRequired(true);
            builder.Property(x => x.PhotoFileName)
                .HasMaxLength(128)
                .IsRequired(true);
        }
    }
}
