﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos.Apartments;
using MVP.Entities.Exceptions;
using System;
using System.Threading.Tasks;

namespace MVP.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IFileReader _fileReader;
        private readonly ILogger<ApartmentController> _logger;
        public ApartmentController(IApartmentService apartmentService, ILogger<ApartmentController> logger, IFileReader fileReader)
        {
            _apartmentService = apartmentService;
            _logger = logger;
            _fileReader = fileReader;
        }

        [HttpPost("api/[controller]")]
        public async Task<IActionResult> CreateApartment([FromBody] CreateApartmentDto createApartmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var response = await _apartmentService.CreateApartment(createApartmentDto);
                return Ok(response);
            }
            catch(ApartmentException ex)
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

        [HttpPut("api/[controller]")]
        public async Task<IActionResult> UpdateApartment([FromBody] UpdateApartmentDto updateApartmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var response = await _apartmentService.UpdateApartment(updateApartmentDto);
                return Ok(response);
            }
            catch(ApartmentException ex)
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

        [HttpPost("api/[controller]/{apartmentId}/Calendar")]
        public async Task<IActionResult> UploadCalendar(int apartmentId, IFormFile file)
        {
            try
            {
                if (file.ContentType != "text/csv")
                {
                    return BadRequest("Invalid file format");
                }

                await _fileReader.ReadCalendarFile(apartmentId, file);

                return Ok();
            }
            catch (FileReaderException ex)
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

        [HttpDelete("api/[controller]/{apartmentId}")]
        public async Task<IActionResult> DeleteApartment(int apartmentId)
        {
            try
            {
                await _apartmentService.DeleteApartment(apartmentId);
                return Ok();

            }
            catch (ApartmentException ex)
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

        [HttpGet("api/[controller]")]
        public async Task<IActionResult> GetAllApartments()
        {
            try
            {
                var apartments = await _apartmentService.GetAllApartments();
                return Ok(apartments);

            }
            catch (ApartmentException ex)
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

        [HttpGet("api/[controller]/{apartmentId}")]
        public async Task<IActionResult> GetApartmentById(int apartmentId)
        {
            try
            {
                var apartment = await _apartmentService.GetApartmentById(apartmentId);
                return Ok(apartment);

            }
            catch (ApartmentException ex)
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

        [HttpGet("api/[controller]/{apartmentId}/Rooms")]
        public async Task<IActionResult> GetRooms(int apartmentId)
        {
            try
            {
                var rooms = await _apartmentService.GetRoomsByApartmentId(apartmentId);
                return Ok(rooms);

            }
            catch (ApartmentException ex)
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

        [HttpGet("api/[controller]/{apartmentId}/Room/{roomId}/Calendar")]
        public async Task<IActionResult> GetRoomsCalendar(int apartmentId, int roomId)
        {
            try
            {
                var calendars = await _apartmentService.GetCalendarByRoomAndApartmentId(apartmentId, roomId);
                return Ok(calendars);

            }
            catch (ApartmentException ex)
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
    }
}