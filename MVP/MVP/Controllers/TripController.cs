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
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost("/CreateTrip")]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripDto createTripDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var response = await _tripService.CreateTripAsync(createTripDto);

                return Ok(response);
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
        [HttpDelete("{tripId}")]
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
        [HttpGet]
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
        [HttpGet("/ByUserId/{userId}")]
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

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpPost("/ByUserId/{userId}")]
        public async Task<IActionResult> GetMergeTripsData(int baseTrip, int additionalTrip)
        {
            try
            {
                var mergedTrips = await _tripService.MergeTripsAsync(baseTrip, additionalTrip);

                return Ok(mergedTrips);
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
    }
}
