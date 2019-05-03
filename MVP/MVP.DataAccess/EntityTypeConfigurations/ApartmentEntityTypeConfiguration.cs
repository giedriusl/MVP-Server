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
            builder.Property(x => x.LocationId);
            builder.Property(x => x.OfficeId);

            builder.HasOne(x => x.Location).WithMany().HasForeignKey(x => x.LocationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Rooms);
        }
    }
}
