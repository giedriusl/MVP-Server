using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    public interface ITripApartmentInfoRepository
    {
        Task<TripApartmentInfo> AddTripApartmentInfoAsync(TripApartmentInfo tripApartmentInfo);
        Task<TripApartmentInfo> GetTripApartmentInfoByTripApartmentRoomAndUser(int tripId, int apartmentRoom, string userId);
    }
}
