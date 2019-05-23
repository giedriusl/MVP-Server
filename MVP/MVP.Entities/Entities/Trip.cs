﻿using MVP.Entities.Enums;
using System;
using System.Collections.Generic;
using MVP.Entities.Dtos.Trips;

namespace MVP.Entities.Entities
{
    public class Trip
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public int FromOfficeId { get; set; }
        public int ToOfficeId { get; set; }
        public virtual Office FromOffice { get; set; }
        public virtual Office ToOffice { get; set; }
        public TripStatus TripStatus { get; set; }
        public virtual List<FlightInformation> FlightInformations { get; set; } = new List<FlightInformation>();
        public virtual List<RentalCarInformation> RentalCarInformations { get; set; } = new List<RentalCarInformation>();
        public virtual ICollection<UserTrip> UserTrips { get; set; } = new List<UserTrip>();


        public void UpdateTrip(UpdateTripDto updateTripDto)
        {
            Title = updateTripDto.Title;
            Start = updateTripDto.Start;
            End = updateTripDto.End;
            FromOfficeId = updateTripDto.FromOfficeId;
            ToOfficeId = updateTripDto.ToOfficeId;
            TripStatus = updateTripDto.TripStatus;
        }
    }
}
