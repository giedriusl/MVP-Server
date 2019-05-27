using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class FlightInformationEntityTypeConfiguration : IEntityTypeConfiguration<FlightInformation>
    {
        public void Configure(EntityTypeBuilder<FlightInformation> builder)
        {
            builder.ToTable(nameof(FlightInformation));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Start).IsRequired();
            builder.Property(x => x.End).IsRequired();
            builder.Property(x => x.Status);
            builder.Property(x => x.TripId).IsRequired();
            builder.Property(x => x.Cost);
            builder.Property(x => x.FromAirport).IsRequired();
            builder.Property(x => x.ToAirport).IsRequired();
        }
    }
}
