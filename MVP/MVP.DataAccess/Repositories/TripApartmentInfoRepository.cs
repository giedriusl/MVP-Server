using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<TripApartmentInfo> GetTripApartmentInfoByIdAsync(int tripApartmentInfoId)
        {
            var tripApartmentInfo = await _context.TripApartmentInfos
                .Include(ta => ta.Calendar)
                .FirstOrDefaultAsync(ta => ta.Id == tripApartmentInfoId);

            return tripApartmentInfo;
        }

        public async Task DeleteAsync(TripApartmentInfo tripApartmentInfo)
        {
            _context.TripApartmentInfos.Remove(tripApartmentInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TripApartmentInfo>> GetTripApartmentInfosByTripAndUserAsync(int tripId, string userId)
        {
            var tripApartmentInfos = await _context.TripApartmentInfos
                .Include(ta => ta.Calendar)
                .Where(ta => ta.TripId == tripId && ta.UserId == userId)
                .ToListAsync();

            return tripApartmentInfos;
        }
    }
}
