using Microsoft.AspNetCore.Identity;
using MVP.BusinessLogic.Helpers.TokenGenerator;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Entities;
using MVP.Entities.Exceptions;
using MVP.Entities.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenGenerator _tokenGenerator;

        public UserService(UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> CreateAsync(NewUserDto newUserDto)
        {
            var user = NewUserDto.ToEntity(newUserDto);

            var identityResult = await _userManager.CreateAsync(user, newUserDto.Password);

            if (!identityResult.Succeeded)
            {
                throw new InvalidUserException("User creation failed on CreateAsync.");
            }

            await _signInManager.SignInAsync(user, isPersistent:false);
            var token = _tokenGenerator.GenerateToken(user);

            return token;
        }

        public async Task<string> LoginAsync(UserLoginDto userLoginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password, 
                userLoginDto.RememberMe, false);

            if (!result.Succeeded)
            {
                throw new InvalidUserException("Invalid login attempt");
            }

            var user = _userManager.Users.First(u => u.Email == userLoginDto.Email);
            var token = _tokenGenerator.GenerateToken(user);

            return token;
        }
    }
}
