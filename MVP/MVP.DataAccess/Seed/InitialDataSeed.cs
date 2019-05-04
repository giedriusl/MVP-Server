using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MVP.Entities.Entities;
using MVP.Entities.Enums;
using System;
using System.Threading.Tasks;

namespace MVP.DataAccess.Seed
{
    public static class InitialDataSeed
    {
        public static async Task SeedAsync(MvpContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();
            var logger = serviceProvider.GetService<ILogger<MvpContext>>();
            await SeedTrip(context, logger);
        }
        private static async Task<(Location, Location)> SeedLocations(MvpContext context, ILogger<MvpContext> logger)
        {
            try
            {
                var firstLocations = await context.Locations
                    .ToListAsync();

                var locationEntity1 = new Location();
                var locationEntity2 = new Location();

                if (firstLocations.Count < 2)
                {
                    var location1 = new Location
                    {
                        City = DefaultLocation.City + "1",
                        CountryCode = DefaultLocation.CountryCode,
                        Address = DefaultLocation.Address + "1"
                    };
                    var location2 = new Location
                    {
                        City = DefaultLocation.City + "2",
                        CountryCode = DefaultLocation.CountryCode,
                        Address = DefaultLocation.Address + "2"
                    };

                    locationEntity1 = context.Locations.Add(location1).Entity;
                    locationEntity2 = context.Locations.Add(location2).Entity;
                    await context.SaveChangesAsync();
                }

                return (locationEntity1, locationEntity2);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to seed default Locations");
                throw;
            }
        }
        private static async Task<(Office, Office)> SeedOffices(MvpContext context, ILogger<MvpContext> logger)
        {
            try
            {
                var firstOffices = await context.Offices
                    .ToListAsync();
                var location = await SeedLocations(context, logger);
                var apartment = await SeedApartment(context, logger, location.Item1.Id);
                var officeEntity1 = new Office();
                var officeEntity2 = new Office();

                if (firstOffices.Count < 2)
                {
                    var office1 = new Office
                    {
                        Name = DefaultOffice.Name + "1",
                        LocationId = location.Item1.Id
                    };
                    office1.Apartments.Add(apartment);

                    var office2 = new Office
                    {
                        Name = DefaultOffice.Name + "2",
                        LocationId = location.Item2.Id
                    };
                    office2.Apartments.Add(apartment);

                    officeEntity1 = context.Offices.Add(office1).Entity;
                    officeEntity2 = context.Offices.Add(office2).Entity;
                    await context.SaveChangesAsync();
                }

                return (officeEntity1, officeEntity2);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to seed default Calendar");
                throw;
            }
        }
        private static async Task<Apartment> SeedApartment(MvpContext context, ILogger<MvpContext> logger, int locationId)
        {
            try
            {
                var firstApartment = await context.Apartments
                    .FirstOrDefaultAsync();

                if (firstApartment is null)
                {
                    var apartment = new Apartment
                    {
                        Title = DefaultApartment.Title,
                        BedCount = DefaultApartment.BedCount,
                        LocationId = locationId
                    };
                    var room = new ApartmentRoom
                    {
                        BedCount = DefaultApartmentRoom.BedCount,
                        Title = DefaultApartmentRoom.Title,
                        RoomNumber = DefaultApartmentRoom.RoomNumber,
                    };
                    var calendar = new Calendar
                    {
                        Start = DefaultCalendar.Start,
                        End = DefaultCalendar.End,
                    };
                    room.Calendars.Add(calendar);
                    apartment.Rooms.Add(room);

                    firstApartment = context.Apartments.Add(apartment).Entity;
                    await context.SaveChangesAsync();
                }

                return firstApartment;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to seed default Calendar");
                throw;
            }
        }
        private static async Task<Trip> SeedTrip(MvpContext context, ILogger<MvpContext> logger)
        {
            try
            {
                var firstTrip = await context.Trips
                    .FirstOrDefaultAsync();
                var offices = await SeedOffices(context, logger);

                if (firstTrip is null)
                {
                    var trip = new Trip
                    {
                        Start = DefaultTrip.Start,
                        End = DefaultTrip.End,
                        Title = DefaultTrip.Title,
                        TripStatus = TripStatus.WaitingForApproval,
                        ToOfficeId = offices.Item1.Id,
                        FromOfficeId = offices.Item2.Id
                    };
                    var flightInfo = new FlightInformation
                    {
                        Cost = DefaultFlightInformation.Cost,
                        Start = DefaultFlightInformation.Start,
                        End = DefaultFlightInformation.End
                    };
                    var carInfo = new RentalCarInformation
                    {
                        Cost = DefaultRentalCarInformation.Cost,
                        Start = DefaultRentalCarInformation.Start,
                        End = DefaultRentalCarInformation.End
                    };

                    trip.FlightInformations.Add(flightInfo);
                    trip.RentalCarInformations.Add(carInfo);

                    firstTrip = context.Trips.Add(trip).Entity;

                    await context.SaveChangesAsync();
                }

                return firstTrip;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to seed default Calendar");
                throw;
            }
        }
    }
}
