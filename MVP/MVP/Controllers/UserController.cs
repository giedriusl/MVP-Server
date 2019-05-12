using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos;
using MVP.Entities.Exceptions;
using System;
using System.Threading.Tasks;

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
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto newUserDto)
        {
            try
            {
                await _userService.CreateAsync(newUserDto);

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
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            try
            {
                var token = await _userService.LoginAsync(userLogin);

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
                await _userService.ResetPassword(resetPasswordDto);
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
                await _userService.SendResetPasswordLink(email);

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
    }
}