using Microsoft.AspNetCore.Http;
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

namespace MVP.BusinessLogic.Services
{
    public class CsvReaderService : IFileReader
    {
        private const int StartDateTimePosition = 0;
        private const int EndDateTimePosition = 1;
        private const int UserIdPosition = 2;
        private const int NamePosition = 0;
        private const int SurnamePosition = 1;
        private const int EmailPosition = 2;
        private const int RolePosition = 3;


        private readonly IApartmentRepository _apartmentRepository;

        public CsvReaderService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<IEnumerable<Calendar>> ReadApartmentCalendarFileAsync(int apartmentId, IFormFile file)
        {
            try
            {
                var data = await ReadData(file);
                var roomNumbers = new List<int>();
                data.ForEach(l => roomNumbers.Add(Int32.Parse(l[2])));

                var rooms = await _apartmentRepository.GetApartmentRoomsByNumberAsync(apartmentId, roomNumbers);

                var calendars = new List<Calendar>();

                if (rooms.Count == 0)
                {
                    throw new FileReaderException("Apartment Rooms were not found");
                }

                foreach (var line in data)
                {
                    var room = rooms.FirstOrDefault(r => r.Id == Int32.Parse(line[UserIdPosition]));

                    if (room != null)
                    {
                        calendars.Add(new Calendar
                        {
                            Start = DateTimeOffset.Parse(line[StartDateTimePosition]),
                            End = DateTimeOffset.Parse(line[EndDateTimePosition]),
                            ApartmentRoomId = room.Id
                        });
                    }
                }

                return calendars;
            }
            catch
            {
                throw new FileReaderException($"Exception while reading {file.FileName} file", "invalidFile");
            }

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
                    calendars.Add(new Calendar
                    {
                        Start = DateTimeOffset.Parse(line[StartDateTimePosition]),
                        End = DateTimeOffset.Parse(line[EndDateTimePosition]),
                        UserId = line[UserIdPosition]
                    });
                }

                return calendars;
            }
            catch
            {
                throw new FileReaderException( $"Exception while reading {file.FileName} file", "invalidFile");
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
