using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(255).IsRequired();

            builder.HasMany(user => user.Calendars)
                .WithOne()
                .HasForeignKey(calendar => calendar.UserId);

            builder.HasMany(user => user.Claims)
                .WithOne()
                .HasForeignKey(userClaim => userClaim.UserId)
                .IsRequired();

            builder.HasMany(user => user.Tokens)
                .WithOne()
                .HasForeignKey(userToken => userToken.UserId)
                .IsRequired();

            builder.HasMany(user => user.UserRoles)
                .WithOne()
                .HasForeignKey(userRole => userRole.UserId)
                .IsRequired();
        }
    }
}
