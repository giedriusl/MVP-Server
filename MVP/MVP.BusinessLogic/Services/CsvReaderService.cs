using Microsoft.AspNetCore.Http;
using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using MVP.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace MVP.BusinessLogic.Services
{
    public class CsvReaderService : IFileReader
    {
        private const int StartDateTimePosition = 0;
        private const int EndDateTimePosition = 1;
        private const int UserIdPosition = 2;

        private readonly ICalendarRepository _calendarRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public CsvReaderService(ICalendarRepository calendarRepository, IApartmentRepository apartmentRepository)
        {
            _calendarRepository = calendarRepository;
            _apartmentRepository = apartmentRepository;
        }

        public async Task ReadApartmentCalendarFile(int apartmentId, IFormFile file)
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
                    var room = rooms.FirstOrDefault(r => r.Id == Int32.Parse(line[2]));

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

                if (calendars.Count > 0)
                {
                    await _calendarRepository.AddCalendarListAsync(calendars);
                }
            }
            catch (Exception ex)
            {
                throw new FileReaderException(ex, $"Exception while reading {file.FileName} file");
            }

        }

        public async Task<List<Calendar>> ReadUsersCalendarFile(IFormFile file)
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
