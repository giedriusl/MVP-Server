﻿using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MVP.BusinessLogic.Helpers.TokenGenerator;
using MVP.BusinessLogic.Helpers.UrlBuilder;
using MVP.BusinessLogic.Interfaces;
using MVP.EmailService.Interfaces;
using MVP.Entities.Dtos;
using MVP.Entities.Entities;
using MVP.Entities.Enums;
using MVP.Entities.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Users;

namespace MVP.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmailManager _emailManager;
        private readonly IUrlBuilder _urlBuilder;
        private readonly IConfiguration _configuration;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IFileReader _csvReaderService;

        public UserService(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ITokenGenerator tokenGenerator, 
            IEmailManager emailManager, 
            IUrlBuilder urlBuilder, 
            IConfiguration configuration,
            ICalendarRepository calendarRepository,
            IFileReader csvReaderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _emailManager = emailManager;
            _urlBuilder = urlBuilder;
            _configuration = configuration;
            _calendarRepository = calendarRepository;
            _csvReaderService = csvReaderService;
        }

        public async Task SendResetPasswordLinkAsync(string email)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            if (user is null)
            {
                throw new InvalidUserException($"User with email {email} was not found.");
            }

            await SendResetPasswordLinkAsync(user);
        }

        public async Task CreateAsync(CreateUserDto createUserDto)
        {
            var user = CreateUserDto.ToEntity(createUserDto);
            var temporaryPassword = _configuration["TemporaryPassword"];
            var identityResult = await _userManager.CreateAsync(user, temporaryPassword);

            if (!identityResult.Succeeded)
            {
                throw new InvalidUserException("User creation failed on CreateAsync.");
            }

            await AssignUserToRole(user, createUserDto.Role);

            await SendResetPasswordLinkAsync(user);
        }

        public async Task<string> LoginAsync(UserDto userDto)
        {
            var result = await _signInManager.PasswordSignInAsync(userDto.Email, userDto.Password, 
                false, false);

            if (!result.Succeeded)
            {
                throw new InvalidUserException("Invalid login attempt");
            }

            var user = _userManager.Users.First(u => u.Email == userDto.Email);
            var token = await _tokenGenerator.GenerateToken(user);

            return token;
        }

        public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = _userManager.Users.First(u => u.Email == resetPasswordDto.Email);
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);

            if (!result.Succeeded)
            {
                throw new InvalidUserException("Password reset failed.");
            }
        }

        public async Task UploadUsersCalendarAsync(IFormFile file)
        {
            var calendars = await _csvReaderService.ReadUsersCalendarFileAsync(file);
            await _calendarRepository.AddCalendarsAsync(calendars);
        }

        private async Task SendResetPasswordLinkAsync(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = _urlBuilder.BuildPasswordResetLink(token, user.Email);
            _emailManager.SendInvitationEmail(user.Email, url);
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
