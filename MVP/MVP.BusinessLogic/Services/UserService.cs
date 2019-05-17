﻿using Microsoft.AspNetCore.Identity;
using MVP.BusinessLogic.Helpers.TokenGenerator;
using MVP.BusinessLogic.Interfaces;
using MVP.Entities.Dtos;
using MVP.Entities.Entities;
using MVP.Entities.Enums;
using MVP.Entities.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using MVP.EmailService.Interfaces;

namespace MVP.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmailManager _emailManager;

        public UserService(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ITokenGenerator tokenGenerator, 
            IEmailManager emailManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _emailManager = emailManager;
        }

        public async Task<string> CreateAsync(CreateUserDto newUserDto)
        {
            var user = CreateUserDto.ToEntity(newUserDto);

            var identityResult = await _userManager.CreateAsync(user, newUserDto.Password);

            if (!identityResult.Succeeded)
            {
                throw new InvalidUserException("User creation failed on CreateAsync.");
            }

            await AssignUserToRole(user, newUserDto.Role);

            await _signInManager.SignInAsync(user, isPersistent:false);
            var token = await _tokenGenerator.GenerateToken(user);

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
            var token = await _tokenGenerator.GenerateToken(user);

            return token;
        }

        public void SendEmail(string url, string email)
        {
            _emailManager.SendConfirmationEmail(email, url);
        }

        private async Task AssignUserToRole(User user, UserRoles role)
        {
            var roleName = role.ToString();
            
            var identityResult = await _userManager.AddToRoleAsync(user, roleName);

            if (!identityResult.Succeeded)
            {
                throw new InvalidUserException("Assigning user to role failed.");
            }
        }
    }
}
