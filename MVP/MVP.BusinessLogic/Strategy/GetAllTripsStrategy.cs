using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using MVP.Entities.Enums;
using System;

namespace MVP.BusinessLogic.Strategy
{
    public class GetAllTripsStrategy
    {
        public static TripsStrategy GetAllTrips(User user, ITripRepository tripRepository, string userRole)
        {
            if (userRole == UserRoles.User.ToString())
            {
                return new UserTrips(tripRepository, user);
            }

            if (userRole == UserRoles.Organizer.ToString())
            {
                return new OrganizerTrips(tripRepository, user);
            }

            if (userRole == UserRoles.Administrator.ToString())
            {
                return new AdminTrips(tripRepository, user);
            }

            throw new NotSupportedException("User role is not supported");
        }
    }
}
