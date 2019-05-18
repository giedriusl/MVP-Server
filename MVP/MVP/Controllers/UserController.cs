using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos;
using MVP.Entities.Exceptions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MVP.Entities.Dtos.Users;

namespace MVP.Controllers
{
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, 
            ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                await _userService.CreateAsync(createUserDto);

                return Ok();
            }
            catch (InvalidUserException exception)
            {
                _logger.Log(LogLevel.Warning, $"Invalid user creation request: {exception.Message}");
                return BadRequest($"user.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error,$"Internal error occured: {exception.Message}");
                return StatusCode(500, "common.internal");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserDto userDto)
        {
            try
            {
                var token = await _userService.LoginAsync(userDto);

                return Ok(token);
            }
            catch (InvalidUserException exception)
            {
                _logger.Log(LogLevel.Warning, $"Invalid user creation request: {exception.Message}");
                return BadRequest($"user.invalidLogin");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Warning, $"Invalid user creation request: {exception.Message}");
                return StatusCode(500, "common.internal");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                await _userService.ResetPasswordAsync(resetPasswordDto);
                return Ok();
            }
            catch (InvalidUserException exception)
            {
                _logger.Log(LogLevel.Error, $"Password reset failed. {exception.Message}");
                return BadRequest("invalidPasswordReset");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SendResetPasswordLink(string email)
        {
            try
            {
                await _userService.SendResetPasswordLinkAsync(email);

                return Ok();
            }
            catch (InvalidUserException exception)
            {
                _logger.Log(LogLevel.Warning, $"Invalid user. {exception.Message}");
                return BadRequest("user.notFound");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $"Internal error occured. {exception.Message}");
                return StatusCode(500);
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost("/User/Calendar")]
        public async Task<IActionResult> UploadCalendar(IFormFile file)
        {
            try
            {
                if (file.ContentType != "text/csv")
                {
                    return BadRequest("Invalid file format");
                }

                await _userService.UploadUsersCalendarAsync(file);

                return Ok();
            }
            catch (FileReaderException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment creation request:", exception);
                return BadRequest($"apartment.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $"Internal error occured. {exception.Message}");
                return StatusCode(500);
            }
        }
    }
}