using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Dtos.Trips;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.TripInfo;
using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Interfaces
{
    public interface ITripService
    {
        Task<CreateTripDto> CreateTripAsync(CreateTripDto createTripDto);
        Task DeleteTripAsync(int tripId);
        Task AddFlightInformationToTripAsync(int tripId, FlightInformationDto flightInformationDto);
        Task AddRentalCarInformationToTripAsync(int tripId, RentalCarInformationDto rentalCarInformationDto);
        Task UpdateRentalCarInformationForTripAsync(int tripId,
            UpdateRentalCarInformationDto updateRentalCarInformationDto);
        Task DeleteRentalCarInformationFromTripAsync(int tripId, int rentalCarInformationId);
        Task UpdateTripAsync(int tripId, CreateTripDto updateTripDto);
        Task DeleteFlightInformationFromTripAsync(int tripId, int flightInformationId);
        Task UpdateFlightInformationForTripAsync(int tripId, UpdateFlightInformationDto updateFlightInformationDto);

        Task<IEnumerable<TripDto>> GetAllTripsAsync();
        Task<TripViewDto> GetTripByIdAsync(int tripId);
        Task<IEnumerable<TripDto>> GetTripsByUserIdAsync(string userId);
        Task<MergedTripDto> GetMergedTripsDataAsync(int baseTripId, int additionalTripId);
        Task<CreateTripDto> MergeTripsAsync(MergedTripDto mergedTripDto);
        Task<IEnumerable<TripViewDto>> GetSimilarTripsAsync(int tripId);

        IEnumerable<TripStatusDto> GetTripStatuses();
        IEnumerable<RentalCarStatusDto> GetRentalCarStatuses();
        IEnumerable<FlightInformationStatusDto> GetFlightInformationStatuses();
        Task<IEnumerable<UserDto>> GetTripUsers(int tripId);
        Task<IEnumerable<FlightInformationDto>> GetTripsFlightInformationsAsync(int tripId);
        Task<IEnumerable<RentalCarInformationDto>> GetTripsRentalCarInformationsAsync(int tripId);

        Task<IEnumerable<CreateTripDto>> GetMergableTrips();
        Task ConfirmAsync(int tripId, string userEmail);

        Task<TripApartmentInfoDto> AddUsersToRooms(UserToRoomDto userToRoom);
    }
}
