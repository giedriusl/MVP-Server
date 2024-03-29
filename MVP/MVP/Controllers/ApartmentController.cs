﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos.Apartments;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Exceptions;
using MVP.Filters;
using System;
using System.Threading.Tasks;

namespace MVP.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize]
    [LoggerFilter]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly ILogger<ApartmentController> _logger;


        public ApartmentController(IApartmentService apartmentService, ILogger<ApartmentController> logger)
        {
            _apartmentService = apartmentService;
            _logger = logger;
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpPost("api/[controller]")]
        public async Task<IActionResult> CreateApartment([FromBody] CreateApartmentDto createApartmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                var response = await _apartmentService.CreateApartmentAsync(createApartmentDto);
                return Ok(response);
            }
            catch(BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment creation request:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpPost("api/[controller]/{apartmentId}/AddRoom")]
        public async Task<IActionResult> AddRoomToApartment(int apartmentId, [FromBody] CreateApartmentRoomDto createRoomDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                await _apartmentService.AddRoomToApartmentAsync(apartmentId, createRoomDto);
                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Could not add room to apartment:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut("api/[controller]/{apartmentId}")]
        public async Task<IActionResult> UpdateApartment(int apartmentId, [FromBody] ApartmentDto updateApartmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                var response = await _apartmentService.UpdateApartmentAsync(apartmentId, updateApartmentDto);
                return Ok(response);
            }
            catch(BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment creation request:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("api/[controller]/{apartmentId}")]
        public async Task<IActionResult> DeleteApartment(int apartmentId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                await _apartmentService.DeleteApartmentAsync(apartmentId);
                return Ok();

            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment deletion request:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]")]
        public async Task<IActionResult> GetAllApartments()
        {
            try
            {
                var apartments = await _apartmentService.GetAllApartmentsAsync();
                return Ok(apartments);

            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartments get request:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]/{apartmentId}")]
        public async Task<IActionResult> GetApartmentById(int apartmentId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                var apartment = await _apartmentService.GetApartmentByIdAsync(apartmentId);
                return Ok(apartment);

            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment get request:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]/{apartmentId}/Rooms")]
        public async Task<IActionResult> GetRooms(int apartmentId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                var rooms = await _apartmentService.GetRoomsByApartmentIdAsync(apartmentId);
                return Ok(rooms);

            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment rooms get request:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]/{apartmentId}/Room/{roomId}/Calendar")]
        public async Task<IActionResult> GetRoomsCalendar(int apartmentId, int roomId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                var calendars = await _apartmentService.GetCalendarByRoomAndApartmentIdAsync(apartmentId, roomId);
                return Ok(calendars);

            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment rooms calendar get request:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost("api/[controller]/UploadCalendar")]
        public async Task<IActionResult> UploadApartmentRoomsCalendar(IFormFile file)
        {
            try
            {
                await _apartmentService.UploadApartmentRoomsCalendarAsync(file);

                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid upload apartment rooms calendar request: ", exception);
                return BadRequest($"apartment.{exception.ErrorCode}");
            }
            catch (FileReaderException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid upload apartment rooms calendar file: ", exception);
                return BadRequest($"file.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "Internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]/OfficeApartments/{officeId}")]
        public async Task<IActionResult> GetAllOfficeApartments(int officeId)
        {
            try
            {
                var apartments = await _apartmentService.GetAllOfficeApartmentsAsync(officeId);

                return Ok(apartments);
            }
            catch (BusinessLogicException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid get apartments request: ", exception);
                return BadRequest($"apartment.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "Internal error occured: ", exception);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpGet("api/[controller]/{apartmentId}/AvailableRooms/{tripId}")]
        public async Task<IActionResult> GetAvailableRooms(int apartmentId, int tripId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                var rooms = await _apartmentService.GetAvailableRooms(apartmentId, tripId);
                return Ok(rooms);
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment rooms get request:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireOrganizerRole")]
        [HttpDelete("api/[controller]/Room/{apartmentRoomId}")]
        public async Task<IActionResult> DeleteRoom(int apartmentRoomId)
        {
            try
            {
                await _apartmentService.DeleteRoomAsync(apartmentRoomId);
                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid room delete request:", ex);
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