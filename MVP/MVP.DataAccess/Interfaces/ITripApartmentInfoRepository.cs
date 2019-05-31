using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface ITripApartmentInfoRepository
    {
        Task<TripApartmentInfo> AddTripApartmentInfoAsync(TripApartmentInfo tripApartmentInfo);
        Task<TripApartmentInfo> GetTripApartmentInfoByIdAsync(int tripApartmentInfoId);
        Task DeleteAsync(TripApartmentInfo tripApartmentInfo);
        Task<IEnumerable<TripApartmentInfo>> GetTripApartmentInfosByTripAndUserAsync(int tripId, string userId);
    }
}
