﻿using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.DataAccess.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly MvpContext _context;

        public ApartmentRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task<Apartment> AddApartmentAsync(Apartment apartment)
        {
            var apartmentEntity =  _context.Apartments.Add(apartment).Entity;
            await _context.SaveChangesAsync();

            return apartmentEntity;
        }

        public async Task UpdateApartmentAsync(Apartment apartment)
        {
             _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteApartmentAsync(Apartment apartment)
        {
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
        }

        public async Task<Apartment> GetApartmentByIdAsync(int apartmentId)
        {
            var apartment = await _context.Apartments
                .Include(a => a.Location)
                .Include(a => a.Office)
                .FirstOrDefaultAsync(x => x.Id == apartmentId);

            return apartment;
        }

        public async Task<Apartment> GetApartmentWithRoomsByIdAsync(int apartmentId)
        {
            var apartment = await _context.Apartments
                .Include(a => a.Rooms)
                .FirstOrDefaultAsync(a => a.Id == apartmentId);

            return apartment;
        }

        public async Task<List<ApartmentRoom>> GetApartmentRoomsByNumberAsync(int apartmentId, List<int> roomNumbers)
        {
            var apartment = await _context.Apartments
                .Where(a => a.Id == apartmentId)
                .SelectMany(r => r.Rooms)
                .Where(a => roomNumbers.Contains(a.RoomNumber))
                .ToListAsync();

            return apartment;
        }

        public async Task<IEnumerable<Apartment>> GetAllApartmentsAsync()
        {
            var apartment = await _context.Apartments
                .Include(a => a.Location)
                .ToListAsync();

            return apartment;
        }

        public async Task<IEnumerable<ApartmentRoom>> GetRoomsByApartmentIdAsync(int apartmentId)
        {
            var rooms = await _context.Apartments
                .Where(a => a.Id == apartmentId)
                .SelectMany(a => a.Rooms)
                .ToListAsync();

            return rooms;
        }

        public async Task<Apartment> GetApartmentByRoomIdAsync(int roomId)
        {
            var apartment = await _context.Apartments
                .Include(a => a.Rooms)
                .Where(a => a.Rooms.Any(r => r.Id == roomId))
                .FirstOrDefaultAsync();

            return apartment;
        }

        public async Task<ApartmentRoom> GetRoomByIdAsync(int apartmentRoomId)
        {
            var apartmentRoom = await _context.ApartmentRooms
                .FirstOrDefaultAsync(r => r.Id == apartmentRoomId);

            return apartmentRoom;
        }

        public async Task DeleteApartmentRoomAsync(ApartmentRoom apartmentRoom)
        {
            _context.ApartmentRooms.Remove(apartmentRoom);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApartmentRoom>> GetRoomsByApartmentIdAndDateAsync(int apartmentId, DateTimeOffset start, DateTimeOffset end)
        {
            var rooms = await _context.Apartments
                .Where(a => a.Id == apartmentId)
                .SelectMany(a => a.Rooms)
                .Where(r => r.Calendars.All(c => !(start < c.End && c.Start < end)))
                .ToListAsync();

            return rooms;
        }

        public async Task<IEnumerable<Apartment>> GetApartmentsByOfficeId(int officeId)
        {
            var apartments = await _context.Apartments
                .Where(apartment => apartment.OfficeId == officeId)
                .Include(apartment => apartment.Location)
                .ToListAsync();

            return apartments;
        }

        public async Task<List<Apartment>> GetAllApartmentsWithRoomsAsync()
        {
            var apartmentsWithRooms = await _context.Apartments
                .Include(apartment => apartment.Rooms)
                .ToListAsync();

            return apartmentsWithRooms;
        }
    }
}
