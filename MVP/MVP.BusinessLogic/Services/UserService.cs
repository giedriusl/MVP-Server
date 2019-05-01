using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Entities;
using MVP.Entities.Exceptions;
using MVP.Entities.Models;

namespace MVP.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> CreateAsync(NewUserDto newUserDto)
        {
            var user = NewUserDto.ToEntity(newUserDto);

            var identityResult = await _userManager.CreateAsync(user, newUserDto.Password);

            if (!identityResult.Succeeded)
            {
                throw new InvalidUserException("User creation failed on CreateAsync.");
            }

            await _signInManager.SignInAsync(user, isPersistent:false);

            return user;
        }

    }
}
