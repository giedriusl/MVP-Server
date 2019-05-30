using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Trips;
using MVP.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Strategy
{
    public class GetAllUserTrips : GetAllTripsStrategy
    {
        public GetAllUserTrips(ITripRepository tripRepository, User user) : base(tripRepository, user)
        {
        }

        public override async Task<IEnumerable<Trip>> GetAllTrips()
        {
            return await TripRepository.GetTripsByUserIdAsync(User.Id);
        }
    }
}
