using Microsoft.AspNetCore.Http;
using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;
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
                var rooms = await _apartmentRepository.GetApartmentRoomsByNumberAsync(apartmentId, roomNumbers);
                data.ForEach(l => roomNumbers.Add(Int32.Parse(l[2])));
                var calendars = new List<Calendar>();

                if (rooms.Count == 0)
                {
                    throw new FileReaderException("Apartment Rooms were not found");
                }

                foreach (var line in data)
                {
                    var room = rooms.FirstOrDefault(r => r.Id == Int32.Parse(line[2]));

                    if (room != null)
                    {
                        calendars.Add(new Calendar
                        {
                            Start = DateTimeOffset.Parse(line[0]),
                            End = DateTimeOffset.Parse(line[1]),
                            ApartmentRoomId = room.Id
                        });
                    }
                }

                return calendars;
            }
            catch (Exception ex)
            {
                throw new FileReaderException(ex, $"Exception while reading {file.FileName} file");
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
                        Name = line[0],
                        Surname = line[1],
                        Email = line[2]
                    });
                }

                return users;
            }
            catch (Exception ex)
            {
                throw new FileReaderException(ex, $"Exception while reading {file.FileName} file");
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
            catch (Exception ex)
            {
                throw new FileReaderException(ex, $"Exception while reading {file.FileName} file");
            }
        }
    }
}
