using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MVP.DataAccess.EntityTypeConfigurations;
using System.IO;

namespace MVP.DataAccess
{
    public class MvpContext : DbContext
    {
        public MvpContext(DbContextOptions<MvpContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("mvp");

            modelBuilder.ApplyConfiguration(new ApartmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ApartmentRoomEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CalendarEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FlightInformationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RentalCarInformationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TripEntityTypeConfiguration());
        }
    }

    public class MvpContextFactory : IDesignTimeDbContextFactory<MvpContext>
    {
        public MvpContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<MvpContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new MvpContext(builder.Options);
        }
    }
}
