using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class UserTripEntityTypeConfiguration : IEntityTypeConfiguration<UserTrip>
    {
        public void Configure(EntityTypeBuilder<UserTrip> builder)
        {
            builder.HasKey(ut => new { ut.UserId, ut.TripId });

            builder
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTrips)
                .HasForeignKey(ut => ut.UserId);

            builder
                .HasOne(ut => ut.Trip)
                .WithMany(u => u.UserTrips)
                .HasForeignKey(ut => ut.TripId);
        }
    }
}
