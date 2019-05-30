using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Trips;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Strategy
{
    public class GetAllAdminTrips : GetAllTripsStrategy
    {
        public GetAllAdminTrips(ITripRepository tripRepository, User user) : base(tripRepository, user)
        {
        }

        public override async Task<IEnumerable<Trip>> GetAllTrips()
        {
            return await TripRepository.GetAllTripsAsync();
        }
    }
}
