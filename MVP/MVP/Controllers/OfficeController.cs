using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos.Offices;
using MVP.Entities.Exceptions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        private readonly ILogger<OfficeController> _logger;

        public OfficeController(ILogger<OfficeController> logger, IOfficeService officeService)
        {
            _logger = logger;
            _officeService = officeService;
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost]
        public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeDto createOfficeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var response = await _officeService.CreateOfficeAsync(createOfficeDto);
                return Ok(response);
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment creation request:", ex);
                return BadRequest($"office.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut]
        public async Task<IActionResult> UpdateOffice([FromBody] UpdateOfficeDto updateOfficeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var response = await _officeService.UpdateOfficeAsync(updateOfficeDto);
                return Ok(response);
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid office creation request:", ex);
                return BadRequest($"office.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("/{officeId}")]
        public async Task<IActionResult> DeleteOffice(int officeId)
        {
            try
            {
                await _officeService.DeleteOfficeAsync(officeId);
                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid office creation request:", ex);
                return BadRequest($"office.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "AllowAllRoles")]
        [HttpGet]
        public async Task<IActionResult> GetAllOffices()
        {
            try
            {
                var apartments = await _officeService.GetAllOfficesAsync();
                return Ok(apartments);
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid office get request:", ex);
                return BadRequest($"office.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "AllowAllRoles")]
        [HttpGet("/{officeId}")]
        public async Task<IActionResult> GetOfficeById(int officeId)
        {
            try
            {
                var office = await _officeService.GetOfficeByIdAsync(officeId);
                return Ok(office);
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid office get request:", ex);
                return BadRequest($"office.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "AllowAllRoles")]
        [HttpGet("/ByName/{officeName}")]
        public async Task<IActionResult> GetOfficeByName(string officeName)
        {
            try
            {
                var office = await _officeService.GetOfficeByNameAsync(officeName);
                return Ok(office);
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid office get request:", ex);
                return BadRequest($"office.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }
    }
}