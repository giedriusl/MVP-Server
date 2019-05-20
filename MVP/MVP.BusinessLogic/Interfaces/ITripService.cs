using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MVP.Entities.Dtos.Trips;

namespace MVP.BusinessLogic.Interfaces
{
    public interface ITripService
    {
        Task<CreateTripDto> CreateTripAsync(CreateTripDto createTripDto);
        Task DeleteTripAsync(int tripId);

        Task<IEnumerable<TripViewDto>> GetAllTripsAsync();
        Task<TripViewDto> GetTripByIdAsync(int tripId);
        Task<IEnumerable<TripViewDto>> GetTripsByUserIdAsync(string userId);
        Task<MergedTripDto> MergeTripsAsync(int baseTripId, int additionalTripId);
    }
}
