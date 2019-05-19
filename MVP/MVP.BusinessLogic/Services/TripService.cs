using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Trips;
using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Services
{
    public class TripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly UserManager<User> _userManager;

        public TripService(ITripRepository tripRepository, IOfficeRepository officeRepository, UserManager<User> userManager)
        {
            _tripRepository = tripRepository;
            _officeRepository = officeRepository;
            _userManager = userManager;
        }

        public async Task<Trip> CreateTrip(CreateTripDto createTripDto)
        {
            var trip = CreateTripDto.ToEntity(createTripDto);

            var fromOffice = await _officeRepository.GetOfficeByIdAsync(createTripDto.FromOfficeId);
            var toOffice = await _officeRepository.GetOfficeByIdAsync(createTripDto.ToOfficeId);

            trip.FromOffice = fromOffice;
            trip.ToOffice = toOffice;
        }
    }
}
