using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Dtos.Trips;
using MVP.Entities.Exceptions;
using MVP.Filters;
using System;
using System.Threading.Tasks;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;

namespace MVP.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize]
    [LoggerFilter]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;
        private readonly ILogger<TripController> _logger;


        public TripController(ITripService tripService, ILogger<TripController> logger)
        {
            _tripService = tripService;
            _logger = logger;
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpPost("api/[controller]/CreateTrip")]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripDto createTripDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var trip = await _tripService.CreateTripAsync(createTripDto);

                return Ok(trip);
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid trip creation request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "Internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpDelete("api/[controller]/{tripId}")]
        public async Task<IActionResult> DeleteTrip(int tripId)
        {
            try
            {
                await _tripService.DeleteTripAsync(tripId);

                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid trip deletion request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "Internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]")]
        public async Task<IActionResult> GetAllTrips()
        {
            try
            {
                var trips = await _tripService.GetAllTripsAsync();

                return Ok(trips);
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid trip get request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "Internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]/{tripId}")]
        public async Task<IActionResult> GetTripById(int tripId)
        {
            try
            {
                var trip = await _tripService.GetTripByIdAsync(tripId);

                return Ok(trip);
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid trip get request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "Internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "AllowAllRoles")]
        [HttpGet("api/[controller]/ByUserId/{userId}")]
        public async Task<IActionResult> GetTripsByUserId(string userId)
        {
            try
            {
                var trips = await _tripService.GetTripsByUserIdAsync(userId);

                return Ok(trips);
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid trips get request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [HttpPut("api/[controller]/{tripId}/AddFlight")]
        [Authorize(Policy = "RequireOrganizerRole")]
        public async Task<IActionResult> AddFlightInformationToTrip(int tripId, [FromBody] FlightInformationDto flightInformationDto)
        {
            try
            {
                await _tripService.AddFlightInformationToTripAsync(tripId, flightInformationDto);

                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid add flight information to trip request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [HttpDelete("api/[controller]/{tripId}/DeleteFlight/{flightInformationId}")]
        [Authorize(Policy = "RequireOrganizerRole")]
        public async Task<IActionResult> DeleteFlightInformationFromTrip(int tripId, int flightInformationId)
        {
            try
            {
                await _tripService.DeleteFlightInformationFromTripAsync(tripId, flightInformationId);

                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid delete flight information from trip request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [HttpPut("api/[controller]/{tripId}/UpdateFlight")]
        [Authorize(Policy = "RequireOrganizerRole")]
        public async Task<IActionResult> UpdateFlightInformationForTrip(int tripId,
            [FromBody] UpdateFlightInformationDto updateFlightInformationDto)
        {
            try
            {
                await _tripService.UpdateFlightInformationForTripAsync(tripId, updateFlightInformationDto);

                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid update flight information request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [HttpPut("api/[controller]/{tripId}/AddRentalCar")]
        [Authorize(Policy = "RequireOrganizerRole")]
        public async Task<IActionResult> AddRentalCarInformationToTrip(int tripId, [FromBody] RentalCarInformationDto rentalCarInformationDto)
        {
            try
            {
                await _tripService.AddRentalCarInformationToTripAsync(tripId, rentalCarInformationDto);

                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid add rental car information to trip request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [HttpPut("api/[controller]/{tripId}/UpdateRentalCar")]
        [Authorize(Policy = "RequireOrganizerRole")]
        public async Task<IActionResult> UpdateRentalCarInformationForTrip(int tripId,
            [FromBody] UpdateRentalCarInformationDto updateRentalCarInformationDto)
        {
            try
            {
                await _tripService.UpdateRentalCarInformationForTripAsync(tripId, updateRentalCarInformationDto);

                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid update rental car information for trip request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [HttpDelete("api/[controller]/{tripId}/DeleteRentalCar/{rentalCarInformationId}")]
        [Authorize(Policy = "RequireOrganizerRole")]
        public async Task<IActionResult> DeleteRentalCarInformationFromTrip(int tripId, int rentalCarInformationId)
        {
            try
            {
                await _tripService.DeleteRentalCarInformationFromTripAsync(tripId, rentalCarInformationId);

                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid rental rental car information from trip request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [HttpGet("api/[controller]/TripStatus")]
        [Authorize(Policy = "AllowAllRoles")]
        public IActionResult GetTripStatus()
        {
            var statuses = _tripService.GetTripStatuses();

            return Ok(statuses);
        }

        [HttpGet("api/[controller]/RentalCarInformationStatus")]
        [Authorize(Policy = "AllowAllRoles")]
        public IActionResult GetRentalCarStatus()
        {
            var statuses = _tripService.GetRentalCarStatuses();

            return Ok(statuses);
        }


        [HttpGet("api/[controller]/FlightInformationStatus")]
        [Authorize(Policy = "AllowAllRoles")]
        public IActionResult GetFlightInformationStatus()
        {
            var statuses = _tripService.GetFlightInformationStatuses();

            return Ok(statuses);
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]/GetMergedTripsData/{baseTrip}/{additionalTrip}")]
        public async Task<IActionResult> GetMergedTripsData(int baseTrip, int additionalTrip)
        {
            try
            {
                var mergedTrips = await _tripService.GetMergedTripsDataAsync(baseTrip, additionalTrip);

                return Ok(mergedTrips);
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid get merge data request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpPost("api/[controller]/MergeTrips")]
        public async Task<IActionResult> MergeTrips([FromBody] MergedTripDto mergedTripDto)
        {
            try
            {
                var mergedTrip = await _tripService.MergeTripsAsync(mergedTripDto);

                return Ok(mergedTrip);
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid trips merge request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]/GetSimilarTrips/{tripId}")]
        public async Task<IActionResult> GetSimilarTrips(int tripId)
        {
            try
            {
                var trips = await _tripService.GetSimilarTripsAsync(tripId);

                return Ok(trips);
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid trips merge request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpPut("api/[controller]/UpdateTrip")]
        public async Task<IActionResult> UpdateTrip([FromBody] UpdateTripDto updateTripDto)
        {
            try
            {
                await _tripService.UpdateTripAsync(updateTripDto);

                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid trip update request: ", exception);
                return BadRequest($"trip.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpPost("api/[controller]/AddUsersToRooms")]
        public async Task<IActionResult> AddUsersToRooms([FromBody] UserToRoomDto userToRoom)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                await _tripService.AddUsersToRooms(userToRoom);

                return Ok();

            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Could not add users to rooms:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }
    }
}
