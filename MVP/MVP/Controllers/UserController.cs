using Microsoft.AspNetCore.Mvc;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Exceptions;
using MVP.Entities.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MVP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("CreateUser")]
        [Authorize]
        public async Task<ActionResult> CreateUser([FromBody] NewUserDto newUserDto)
        {
            try
            {
                var token = await _userService.CreateAsync(newUserDto);

                return Ok(token);
            }
            catch (InvalidUserException exception)
            {
                return BadRequest("user.invalidCreation");
            }
            catch (Exception exception)
            {
                //TODO: use exception for logging

                return BadRequest("common.internal");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            try
            {
                var token = await _userService.LoginAsync(userLogin);

                return Ok(token);
            }
            catch (InvalidUserException exception)
            {
                return BadRequest("user.invalidLogin");
            }
            catch (Exception exception)
            {
                //TODO: use exception for logging
                return BadRequest("common.internal");
            }
            
        }
    }
}