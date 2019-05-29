using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Trips;
using MVP.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Strategy
{
    public class GetAllOrganizerTrips : GetAllTripsStrategy
    {
        public GetAllOrganizerTrips(ITripRepository tripRepository, User user) : base(tripRepository, user)
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
