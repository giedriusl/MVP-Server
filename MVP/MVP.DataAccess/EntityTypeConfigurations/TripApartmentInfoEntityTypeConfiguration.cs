using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class TripApartmentInfoEntityTypeConfiguration : IEntityTypeConfiguration<TripApartmentInfo>
    {
        public void Configure(EntityTypeBuilder<TripApartmentInfo> builder)
        {
            builder.ToTable(nameof(TripApartmentInfo));
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Calendar)
                .WithOne(c => c.TripApartmentInfo)
                .HasForeignKey<TripApartmentInfo>(x => x.CalendarId);

            builder.Property(x => x.TripId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ApartmentRoomId).IsRequired();
        }
    }
}
