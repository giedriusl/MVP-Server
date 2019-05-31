using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Strategy
{
    public abstract class TripsStrategy
    {
        protected readonly User User;
        protected readonly ITripRepository TripRepository;
        public abstract Task<IEnumerable<Trip>> GetAllTrips();

        protected TripsStrategy(ITripRepository tripRepository, User user)
        {
            TripRepository = tripRepository;
            User = user;
        }
    }
}
