using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class CalendarEntityTypeConfiguration : IEntityTypeConfiguration<Calendar>
    {
        public void Configure(EntityTypeBuilder<Calendar> builder)
        {
            builder.ToTable(nameof(Calendar));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.ApartmentRoomId);
            builder.HasOne(c => c.User)
                .WithMany(u => u.Calendars)
                .HasForeignKey(c => c.UserId);
            builder.Property(c => c.Start).IsRequired();
            builder.Property(c => c.End).IsRequired();
        }
    }
}
