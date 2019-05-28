using System.Linq;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVP.DataAccess.Repositories
{
    public class TripApartmentInfoRepository : ITripApartmentInfoRepository
    {
        private readonly MvpContext _context;

        public TripApartmentInfoRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task<TripApartmentInfo> AddTripApartmentInfoAsync(TripApartmentInfo tripApartmentInfo)
        {
            var tripApartmentInfoEntity = _context.TripApartmentInfos.Add(tripApartmentInfo).Entity;
            await _context.SaveChangesAsync();

            return tripApartmentInfoEntity;
        }

        public async Task<TripApartmentInfo> GetTripApartmentInfoByTripApartmentRoomAndUser(int tripId, int apartmentRoom, string userId)
        {
            var tripApartmentInfo = await _context.TripApartmentInfos
                .Include(ta => ta.Calendar)
                .FirstOrDefaultAsync(ta => ta.TripId == tripId 
                                           && ta.ApartmentRoomId == apartmentRoom 
                                           && ta.UserId == userId);

            return tripApartmentInfo;
        }
    }
}
