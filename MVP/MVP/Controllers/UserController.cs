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
                    return BadRequest("model.invalid");
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
                    return BadRequest("model.invalid");
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
                _logger.Log(LogLevel.Warning, $"Internal error occured: {exception.Message}");
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
                    return BadRequest("model.invalid");
                }

                await _userService.ResetPasswordAsync(resetPasswordDto);
                return Ok();
            }
            catch (InvalidUserException exception)
            {
                _logger.Log(LogLevel.Error, $"Password reset failed. {exception.Message}");
                return BadRequest("user.invalidPasswordReset");
            }
        }

        [HttpPost("api/[controller]/SendResetPasswordLink")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> SendResetPasswordLink([FromBody] SendResetPasswordDto sendResetPasswordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("model.invalid");
                }

                await _userService.SendResetPasswordLinkAsync(sendResetPasswordDto.Email);

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
                return StatusCode(500, "common.internal");
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
                return StatusCode(500, "common.internal");
            }
        }

        [HttpGet("api/[controller]/{userId}")]
        [Authorize(Policy = "RequireAdministratorRole")]
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
                return StatusCode(500, "common.internal");
            }
        }


        [HttpPost("api/[controller]/UploadUsers")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> UploadUsers(IFormFile file)
        {
            try
            {
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (fileExt != "csv")
                {
                    return BadRequest("file.invalid");
                }

                await _userService.UploadUsersAsync(file);

                return Ok();
            }
            catch (FileReaderException ex)
            {
                _logger.Log(LogLevel.Warning, "Invalid user upload file: ", ex);
                return BadRequest($"file.{ex.ErrorCode}");
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
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (fileExt != "csv")
                {
                    return BadRequest("file.invalid");
                }

                await _userService.UploadUsersCalendarAsync(file);

                return Ok();
            }
            catch (FileReaderException exception)
            {
                _logger.Log(LogLevel.Warning, "Invalid apartment creation request:", exception);
                return BadRequest($"file.{exception.ErrorCode}");
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

        [HttpPost("api/[controller]/ValidateResetPasswordToken")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateResetPasswordToken([FromBody] TokenValidationDto tokenValidation)
        {
            try
            {
                await _userService.ValidateResetPasswordToken(tokenValidation.Email, tokenValidation.Token);
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $"Could not verify token. {exception.Message}");
                return Unauthorized();
            }
        }
    }
}