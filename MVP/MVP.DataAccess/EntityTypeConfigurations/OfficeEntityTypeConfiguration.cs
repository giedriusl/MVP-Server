using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class OfficeEntityTypeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable(nameof(Office));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.LocationId).IsRequired();

            builder.HasOne(x => x.Location).WithMany().HasForeignKey(x => x.LocationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Apartments).WithOne(x => x.Office);
        }
    }
}
