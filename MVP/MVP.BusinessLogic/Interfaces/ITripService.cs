﻿using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Dtos.Trips;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Interfaces
{
    public interface ITripService
    {
        Task<CreateTripDto> CreateTripAsync(CreateTripDto createTripDto);
        Task DeleteTripAsync(int tripId);
        Task<UpdateTripDto> AddFlightInformationToTripAsync(int tripId, FlightInformationDto flightInformatiomDto);

        Task<IEnumerable<TripDto>> GetAllTripsAsync();
        Task<TripViewDto> GetTripByIdAsync(int tripId);
        Task<IEnumerable<TripDto>> GetTripsByUserIdAsync(string userId);

        IEnumerable<TripStatusDto> GetTripStatuses();
        IEnumerable<RentalCarStatusDto> GetRentalCarStatuses();
        IEnumerable<FlightInformationStatusDto> GetFlightInformationStatuses();
    }
}
