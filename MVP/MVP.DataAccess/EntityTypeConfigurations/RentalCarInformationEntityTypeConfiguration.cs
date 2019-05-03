using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    class RentalCarInformationEntityTypeConfiguration : IEntityTypeConfiguration<RentalCarInformation>
    {
        public void Configure(EntityTypeBuilder<RentalCarInformation> builder)
        {
            builder.ToTable(nameof(RentalCarInformation));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Start).IsRequired();
            builder.Property(x => x.End).IsRequired();
            builder.Property(x => x.Status);
            builder.Property(x => x.TripId).IsRequired();
            builder.Property(x => x.Cost);
        }
    }
}
