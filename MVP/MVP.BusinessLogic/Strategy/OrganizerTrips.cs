using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Strategy
{
    public class OrganizerTrips : TripsStrategy
    {
        public OrganizerTrips(ITripRepository tripRepository, User user) : base(tripRepository, user)
        {
        }

        public override async Task<IEnumerable<Trip>> GetAllTrips()
        {
            var participatedTrips = await TripRepository.GetTripsByUserIdAsync(User.Id);
            var organizedTrips = await TripRepository.GetTripsByOrganizerIdAsync(User.Id);
            var allOrganizerTrips = participatedTrips.Union(organizedTrips);

            return allOrganizerTrips;
        }
    }
}
