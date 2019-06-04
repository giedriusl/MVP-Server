using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVP.BusinessLogic.Helpers.TokenGenerator;
using MVP.BusinessLogic.Helpers.UrlBuilder;
using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.EmailService.Interfaces;
using MVP.Entities.Dtos;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;
using MVP.Entities.Enums;
using MVP.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IFileReader _fileReader;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenGenerator tokenGenerator,
            IEmailManager emailManager,
            IUrlBuilder urlBuilder,
            IConfiguration configuration,
            ICalendarRepository calendarRepository,
            IFileReader fileReader)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _emailManager = emailManager;
            _urlBuilder = urlBuilder;
            _configuration = configuration;
            _calendarRepository = calendarRepository;
            _fileReader = fileReader;
        }

        public async Task SendResetPasswordLinkAsync(string email)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            if (user is null)
            {
                throw new InvalidUserException($"User with email {email} was not found.", "userNotFound");
            }

            await SendResetPasswordLinkAsync(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<CreateUserDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = CreateUserDto.ToDto(user);
                 userDto.Role = (UserRoles)Enum.Parse(typeof(UserRoles), roles.First());
                 usersWithRoles.Add(userDto);
            }
            return usersWithRoles;
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return UserDto.ToDto(user);
        }

        public async Task UploadUsersAsync(IFormFile file)
        {
            var users = await _fileReader.ReadUsersFileAsync(file);

            foreach (var user in users)
            {
                await CreateAsync(user);
            }
        }

        public async Task<CreateUserDto> CreateAsync(CreateUserDto createUserDto)
        {
            var user = CreateUserDto.ToEntity(createUserDto);
            var temporaryPassword = _configuration["TemporaryPassword"];
            var identityResult = await _userManager.CreateAsync(user, temporaryPassword);

            if (!identityResult.Succeeded)
            {
                throw new InvalidUserException("User creation failed on CreateAsync.", identityResult.Errors.First().Code);
            }

            user = await _userManager.FindByEmailAsync(createUserDto.Email);
            createUserDto.Id = user.Id;

            await AssignUserToRoleAsync(user, createUserDto.Role);

            await SendResetPasswordLinkAsync(user);

            return createUserDto;
        }

        public async Task<string> LoginAsync(UserLoginDto userDto)
        {
            var result = await _signInManager.PasswordSignInAsync(userDto.Email, userDto.Password,
                false, false);

            if (!result.Succeeded)
            {
                throw new InvalidUserException("Invalid login attempt", "invalidLogin");
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
                throw new InvalidUserException("Password reset failed.", "passwordResetFail");
            }
        }

        public async Task UploadUsersCalendarAsync(IFormFile file)
        {
            var calendars = await _fileReader.ReadUsersCalendarFileAsync(file);
            var validCalendars = new List<Calendar>();

            foreach (var calendar in calendars)
            {
                var user = _userManager.Users.FirstOrDefault(u => u.Id == calendar.UserId);

                if (user != null)
                {
                    validCalendars.Add(calendar);
                }
            }

            await _calendarRepository.AddCalendarsAsync(validCalendars);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new InvalidUserException("There is no user with this email", "noUser");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userDto = CreateUserDto.ToDto(user);

            userDto.Role = (UserRoles)Enum.Parse(typeof(UserRoles), roles.First());
            return userDto;
        }

        public IEnumerable<UserRolesDto> GetUserRoles()
        {
            var roles = Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>();

            return roles.Select(UserRolesDto.ToDto).ToList();
        }

        public async Task ValidateResetPasswordToken(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new BusinessLogicException("User not found.", "userNotFound");
            }

            var valid = await _userManager.VerifyUserTokenAsync(user, this._userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            if (!valid)
            {
                throw new BusinessLogicException("Invalid token.", "invalidToken");
            }
        }

        private async Task SendResetPasswordLinkAsync(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var email = user.Email.Replace(".", "[dot]");
            var url = _urlBuilder.BuildPasswordResetLink(token, email);
            _emailManager.SendInvitationEmail(user.Email, url);
        }

        private async Task AssignUserToRoleAsync(User user, UserRoles role)
        {
            var roleName = role.ToString();
            var identityResult = await _userManager.AddToRoleAsync(user, roleName);

            if (!identityResult.Succeeded)
            {
                throw new InvalidUserException("Assigning user to role failed.", "roleAssignFail");
            }
        }
    }
}
