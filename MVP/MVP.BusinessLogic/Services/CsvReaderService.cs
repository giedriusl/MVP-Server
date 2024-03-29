﻿using Microsoft.AspNetCore.Http;
using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;
using MVP.Entities.Enums;
using MVP.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MVP.BusinessLogic.Services
{
    public class CsvReaderService : IFileReader
    {
        private const int StartDateTimePosition = 0;
        private const int EndDateTimePosition = 1;
        private const int UserEmailPosition = 2;
        private const int NamePosition = 0;
        private const int SurnamePosition = 1;
        private const int EmailPosition = 2;
        private const int RolePosition = 3;


        private readonly IApartmentRepository _apartmentRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly UserManager<User> _userManager;

        public CsvReaderService(IApartmentRepository apartmentRepository, 
            ICalendarRepository calendarRepository, 
            UserManager<User> userManager)
        {
            _apartmentRepository = apartmentRepository;
            _calendarRepository = calendarRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<CreateUserDto>> ReadUsersFileAsync(IFormFile file)
        {
            try
            {
                var data = await ReadData(file);
                var users = new List<CreateUserDto>();
                foreach (var line in data)
                {
                    users.Add(new CreateUserDto
                    {
                        Name = line[NamePosition],
                        Surname = line[SurnamePosition],
                        Email = line[EmailPosition],
                        Role = (UserRoles)Enum.Parse(typeof(UserRoles), line[RolePosition])
                    });
                }

                return users;
            }
            catch
            {
                throw new FileReaderException($"Exception while reading {file.FileName} file", "invalidFile");
            }

        }

        public async Task<IEnumerable<Calendar>> ReadUsersCalendarFileAsync(IFormFile file)
        {
            try
            {
                var data = await ReadData(file);
                var calendars = new List<Calendar>();

                foreach (var line in data)
                {
                    var userEmail = line[UserEmailPosition];
                    var user = await _userManager.FindByEmailAsync(userEmail);
                    calendars.Add(new Calendar
                    {
                        Start = DateTimeOffset.Parse(line[StartDateTimePosition]),
                        End = DateTimeOffset.Parse(line[EndDateTimePosition]),
                        UserId = user.Id
                    });
                }

                return calendars;
            }
            catch
            {
                throw new FileReaderException( $"Exception while reading {file.FileName} file", "invalidFile");
            }
        }

        public async Task<IEnumerable<Calendar>> ReadApartmentRoomCalendarFileAsync(IFormFile file)
        {
            try
            {
                var data = await ReadData(file);
                var calendarsToImport = new List<Calendar>();
                var apartmentsWithRooms = await _apartmentRepository.GetAllApartmentsWithRoomsAsync();

                foreach (var line in data)
                {
                    var endDate = DateTimeOffset.Parse(line[(int) ImportApartmentRoomSchedule.EndDate]);
                    var startDate = DateTimeOffset.Parse(line[(int) ImportApartmentRoomSchedule.StartDate]);

                    var apartment = apartmentsWithRooms.FirstOrDefault(apart =>
                        apart.Title == line[(int) ImportApartmentRoomSchedule.ApartmentTitle]);

                    if (apartment is null)
                        continue;


                    var apartmentRoom = apartment.Rooms
                        .FirstOrDefault(room => room.Title == line[(int) ImportApartmentRoomSchedule.RoomTitle]);

                    if (apartmentRoom == null)
                        continue;

                    var calendars = await _calendarRepository.GetCalendarByRoomAndApartmentId(apartment.Id, apartmentRoom.Id);

                    bool calendarOverlaps = calendars.Any(x => (x.Start <= endDate) && (x.End >= startDate));
                    calendarOverlaps = calendarOverlaps || calendarsToImport.Any(x => (x.Start <= endDate) && (x.End >= startDate));

                    if (calendarOverlaps) 
                        continue;
                    

                    calendarsToImport.Add(new Calendar
                    {
                        ApartmentRoomId = apartmentRoom.Id,
                        End = endDate,
                        Start = startDate
                    });
                }

                return calendarsToImport;
            }
            catch (Exception)
            {
                throw new FileReaderException($"Exception while reading {file.FileName} file", "invalidFile");
            }
        }

        private async Task<List<List<string>>> ReadData(IFormFile file)
        {
            try
            {
                var result = new List<List<string>>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = await reader.ReadLineAsync();
                        result.Add(line.Split(',').ToList());
                    }
                }

                if (result.Count == 0)
                {
                    throw new FileReaderException("File is empty.");
                }

                return result;
            }
            catch
            {
                throw new FileReaderException($"Exception while reading {file.FileName} file", "invalidFile");
            }
        }
    }
}
