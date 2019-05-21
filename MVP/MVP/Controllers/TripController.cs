using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos.Trips;
using MVP.Entities.Exceptions;
using System;
using System.Threading.Tasks;

namespace MVP.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize]
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
    }
}
