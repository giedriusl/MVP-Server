using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Exceptions;
using MVP.Filters;
using System;
using System.Threading.Tasks;

namespace MVP.Controllers
{
    [ApiController]
    [Authorize]
    [LoggerFilter]
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

        [HttpPost("api/[controller]/CreateUser")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var user = await _userService.CreateAsync(createUserDto);

                return Ok(user);
            }
            catch (InvalidUserException exception)
            {
                _logger.Log(LogLevel.Warning, $"Invalid user creation request: {exception.Message}");
                return BadRequest($"user.{exception.ErrorCode}");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $"Internal error occured: {exception.Message}");
                return StatusCode(500, "common.internal");
            }
        }

        [HttpPost("api/[controller]/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var token = await _userService.LoginAsync(userLoginDto);

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

        [HttpPost("api/[controller]/ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                await _userService.ResetPasswordAsync(resetPasswordDto);
                return Ok();
            }
            catch (InvalidUserException exception)
            {
                _logger.Log(LogLevel.Error, $"Password reset failed. {exception.Message}");
                return BadRequest("invalidPasswordReset");
            }
        }

        [HttpPost("api/[controller]/SendResetPasswordLink")]
        [AllowAnonymous]
        public async Task<IActionResult> SendResetPasswordLink(string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

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

        [HttpGet("api/[controller]")]
        [Authorize(Policy = "RequireOrganizerRole")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $"Internal error occured. {exception.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("api/[controller]/{userId}")]
        [Authorize(Policy = "RequireOrganizerRole")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var users = await _userService.GetUserByIdAsync(userId);
                return Ok(users);
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $"Internal error occured. {exception.Message}");
                return StatusCode(500);
            }
        }


        [HttpPost("api/[controller]/UploadUsers")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> UploadUsers(IFormFile file)
        {
            try
            {
                if (file.ContentType != "text/csv")
                {
                    return BadRequest("Invalid file format");
                }

                await _userService.UploadUsersAsync(file);

                return Ok();
            }
            catch (FileReaderException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment creation request:", ex);
                return BadRequest($"apartment.{ex.ErrorCode}");
            }
            catch (InvalidUserException exception)
            {
                _logger.Log(LogLevel.Warning, $"Invalid user creation request: {exception.Message}");
                return BadRequest($"user.{exception.ErrorCode}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Internal error occured:", ex);
                return StatusCode(500, "common.internal");
            }
        }

        [HttpPost("api/[controller]/Calendar")]
        [Authorize(Policy = "RequireAdministratorRole")]
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

        [HttpPost("api/[controller]/GetUserIdentity")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.Identity.Name;
            try
            {
                var user = await _userService.GetUserByEmail(email);

                return Ok(user);
            }
            catch (InvalidUserException exception)
            {
                _logger.Log(LogLevel.Warning, $"Invalid user creation request: {exception.Message}");
                return BadRequest($"user.{exception.ErrorCode}");
            }
        }

        [HttpGet("api/[controller]/Roles")]
        [Authorize(Policy = "RequireOrganizerRole")]
        public IActionResult GetUserRoles()
        {
            var roles = _userService.GetUserRoles();

            return Ok(roles);
        }
    }
}