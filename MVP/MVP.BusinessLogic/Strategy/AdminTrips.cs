using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Strategy
{
    public class AdminTrips : TripsStrategy
    {
        public AdminTrips(ITripRepository tripRepository, User user) : base(tripRepository, user)
        {
        }

        public override async Task<IEnumerable<Trip>> GetAllTrips()
        {
            return await TripRepository.GetAllTripsAsync();
        }
    }
}
