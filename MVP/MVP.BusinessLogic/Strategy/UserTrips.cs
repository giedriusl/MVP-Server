using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Strategy
{
    public class UserTrips : TripsStrategy
    {
        public UserTrips(ITripRepository tripRepository, User user) : base(tripRepository, user)
        {
        }

        public override async Task<IEnumerable<Trip>> GetAllTrips()
        {
            return await TripRepository.GetTripsByUserIdAsync(User.Id);
        }
    }
}
