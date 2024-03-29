﻿using Microsoft.AspNetCore.Http;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IFileReader
    {
        Task<IEnumerable<CreateUserDto>> ReadUsersFileAsync(IFormFile file);
        Task<IEnumerable<Calendar>> ReadUsersCalendarFileAsync(IFormFile file);
        Task<IEnumerable<Calendar>> ReadApartmentRoomCalendarFileAsync(IFormFile file);
    }
}
