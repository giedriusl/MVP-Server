using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable(nameof(Location));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.City).HasMaxLength(500).IsRequired();
            builder.Property(x => x.CountryCode).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(500).IsRequired();
        }
    }
}
