using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class ApartmentEntityTypeConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.ToTable(nameof(Apartment));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(256);
            builder.Property(x => x.LocationId).IsRequired();
            builder.HasOne(x => x.Location).WithOne();
            builder.Property(x => x.OfficeId).IsRequired();
            builder.HasMany(x => x.Rooms);
        }
    }
}
