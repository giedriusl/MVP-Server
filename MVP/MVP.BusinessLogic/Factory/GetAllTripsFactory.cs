using MVP.BusinessLogic.Strategy;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System;

namespace MVP.BusinessLogic.Factory
{
    public class GetAllTripsFactory
    {
        public static GetAllTripsStrategy GetAllTrips(User user, ITripRepository tripRepository, string userRole)
        {
            switch (userRole)
            {
                case "User":
                    return new GetAllUserTrips(tripRepository, user);

                case "Organizer":
                    return new GetAllOrganizerTrips(tripRepository, user);

                case "Administrator":
                    return new GetAllAdminTrips(tripRepository, user);

                default:
                    throw new NotSupportedException("User role is not supported");
            }
        }
    }
}
