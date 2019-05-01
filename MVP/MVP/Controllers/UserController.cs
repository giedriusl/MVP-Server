using Microsoft.AspNetCore.Mvc;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Models;
using System;
using System.Threading.Tasks;

namespace MVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ActionResult> CreateUser(NewUserDto newUserDto)
        {
            try
            {
                var user = await _userService.CreateAsync(newUserDto);

                return Ok(user);
            }
            catch (Exception exception)
            {
                //Logging
                return BadRequest("User creation failed.");
            }
        }
    }
}