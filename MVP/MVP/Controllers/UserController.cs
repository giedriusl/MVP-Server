using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Exceptions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVP.Controllers
{
    [ApiController]
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

        [HttpPost("api/[controller]/CreateUser")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

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

        [HttpPost("api/[controller]/Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
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
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
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
        public async Task<ActionResult> SendResetPasswordLink(string email)
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
        public async Task<ActionResult> GetAllUsers()
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
        public async Task<ActionResult> GetUserById(string userId)
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
        public async Task<ActionResult> UploadUsers(IFormFile file)
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
    }
}