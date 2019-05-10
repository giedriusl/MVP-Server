using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class ApartmentRoomEntityTypeConfiguration : IEntityTypeConfiguration<ApartmentRoom>
    {
        public void Configure(EntityTypeBuilder<ApartmentRoom> builder)
        {
            builder.ToTable(nameof(ApartmentRoom));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoomNumber).IsRequired();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
            builder.Property(x => x.BedCount).IsRequired();
            builder.Property(x => x.ApartmentId).IsRequired();

            builder.HasMany(x => x.Calendars);
        }
    }
}
