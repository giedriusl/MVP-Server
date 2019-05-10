using System;

namespace MVP.DataAccess.Seed
{
    public static class DefaultTrip
    {
        public static readonly string Title = "DefaultTrip";
        public static readonly DateTime Start = DateTime.UtcNow;
        public static readonly DateTime End = DateTime.UtcNow.AddDays(2);
    }

    public static class DefaultOffice
    {
        public static readonly string Name = "Default Office ";
    }

    public static class DefaultLocation
    {
        public static readonly string City = "Default City ";
        public static readonly string CountryCode = "Default Country Code";
        public static readonly string Address = "Default Address ";
    }
    public static class DefaultFlightInformation
    {
        public static readonly double Cost = 100;
        public static readonly DateTime Start = DateTime.UtcNow;
        public static readonly DateTime End = DateTime.UtcNow.AddDays(1);
    }
    public static class DefaultRentalCarInformation
    {
        public static readonly double Cost = 100;
        public static readonly DateTime Start = DateTime.UtcNow;
        public static readonly DateTime End = DateTime.UtcNow.AddDays(1);
    }
    public static class DefaultApartment
    {
        public static readonly string Title = "DefaultApartment";
        public static readonly int BedCount = 1;
    }
    public static class DefaultApartmentRoom
    {
        public static readonly string Title = "DefaultApartmentRoom";
        public static readonly int BedCount = 1;
        public static readonly int RoomNumber = 001;
    }
    public static class DefaultCalendar
    {
        public static readonly DateTime Start = DateTime.UtcNow;
        public static readonly DateTime End = DateTime.UtcNow.AddDays(1);
    }
}
