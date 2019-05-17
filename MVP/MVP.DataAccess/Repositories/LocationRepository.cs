using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Threading.Tasks;

namespace MVP.DataAccess.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MvpContext _context;

        public LocationRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task<Location> GetLocationByCityAndCountryCodeAndAddress(string city, string countryCode, string address)
        {
            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.Address == address 
                                          && l.City == city 
                                          && l.CountryCode == countryCode);

            return location;
        }
    }
}
