using MVP.Entities.Entities;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location> GetLocationByCityAndCountryCodeAndAddress(string city, string countryCode, string address);
    }
}
