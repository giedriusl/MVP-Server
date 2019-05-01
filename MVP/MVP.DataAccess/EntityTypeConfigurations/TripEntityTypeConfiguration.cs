using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVP.DataAccess.EntityTypeConfigurations
{
    public class TripEntityTypeConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.ToTable(nameof(Trip));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Start).IsRequired();
            builder.Property(x => x.End).IsRequired();
            builder.Property(x => x.ToOfficeId).IsRequired();
            builder.Property(x => x.FromOfficeId).IsRequired();
            builder.Property(x => x.TripStatus);

            builder.HasOne(x => x.ToOffice).WithMany().HasForeignKey(x => x.ToOfficeId);
            builder.HasOne(x => x.FromOffice).WithMany().HasForeignKey(x => x.FromOfficeId);

            builder.HasMany(x => x.RentalCarInformations);
            builder.HasMany(x => x.FlightInformations);
            builder.HasMany(x => x.Users);
        }
    }
}
