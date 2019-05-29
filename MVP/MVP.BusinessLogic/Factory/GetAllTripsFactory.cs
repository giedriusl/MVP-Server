using MVP.BusinessLogic.Strategy;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System;
using MVP.Entities.Enums;

namespace MVP.BusinessLogic.Factory
{
    public class GetAllTripsFactory
    {
        public static GetAllTripsStrategy GetAllTrips(User user, ITripRepository tripRepository, string userRole)
        {
            if (userRole == UserRoles.User.ToString())
            {
                return new GetAllUserTrips(tripRepository, user);
            }

            if (userRole == UserRoles.Organizer.ToString())
            {
                return new GetAllOrganizerTrips(tripRepository, user);
            }

            if (userRole == UserRoles.Administrator.ToString())
            {
                return new GetAllAdminTrips(tripRepository, user);
            }

            throw new NotSupportedException("User role is not supported");
        }
    }
}
