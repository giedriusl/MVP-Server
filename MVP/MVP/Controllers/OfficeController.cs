using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos.Offices;
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
        [HttpPost("api/[controller]")]
        public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeDto createOfficeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                var response = await _officeService.CreateOfficeAsync(createOfficeDto);
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
        [HttpPut("api/[controller]/{officeId}")]
        public async Task<IActionResult> UpdateOffice(int officeId, [FromBody] OfficeDto updateOfficeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                var response = await _officeService.UpdateOfficeAsync(officeId, updateOfficeDto);
                return Ok(response);
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid office update request:", ex);
                return BadRequest($"office.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("api/[controller]/{officeId}")]
        public async Task<IActionResult> DeleteOffice(int officeId)
        {
            try
            {
                await _officeService.DeleteOfficeAsync(officeId);
                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid office deletion request:", ex);
                return BadRequest($"office.{ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [Authorize(Policy = "AllowAllRoles")]
        [HttpGet("api/[controller]")]
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
        [HttpGet("api/[controller]/{officeId}")]
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
        [HttpGet("api/[controller]/ByName/{officeName}")]
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