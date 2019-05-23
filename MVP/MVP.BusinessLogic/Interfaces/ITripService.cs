using MVP.Entities.Dtos.FlightsInformation;
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
        Task AddFlightInformationToTripAsync(int tripId, FlightInformationDto flightInformationDto);
        Task AddRentalCarInformationToTripAsync(int tripId, RentalCarInformationDto rentalCarInformationDto);
        Task UpdateTripAsync(UpdateTripDto updateTripDto);

        Task<IEnumerable<TripDto>> GetAllTripsAsync();
        Task<TripViewDto> GetTripByIdAsync(int tripId);
        Task<IEnumerable<TripDto>> GetTripsByUserIdAsync(string userId);
        Task<MergedTripDto> GetMergedTripsDataAsync(int baseTripId, int additionalTripId);
        Task<CreateTripDto> MergeTripsAsync(MergedTripDto mergedTripDto);
        Task<IEnumerable<TripViewDto>> GetSimilarTripsAsync(int tripId);

        IEnumerable<TripStatusDto> GetTripStatuses();
        IEnumerable<RentalCarStatusDto> GetRentalCarStatuses();
        IEnumerable<FlightInformationStatusDto> GetFlightInformationStatuses();
        Task ConfirmAsync(int tripId, string userEmail);
    }
}
