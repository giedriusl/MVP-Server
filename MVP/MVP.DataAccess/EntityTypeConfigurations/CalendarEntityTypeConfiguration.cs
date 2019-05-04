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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ApartmentRoomId);
            builder.Property(x => x.Start).IsRequired();
            builder.Property(x => x.End).IsRequired();
        }
    }
}
