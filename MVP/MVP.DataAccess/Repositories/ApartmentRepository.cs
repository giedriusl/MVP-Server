﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly MvpContext _context;

        public ApartmentRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task<Apartment> AddApartment(Apartment apartment)
        {
            var apartmentEntity =  _context.Apartments.Add(apartment).Entity;
            await _context.SaveChangesAsync();

            return apartmentEntity;
        }

        public async Task UpdateApartment(Apartment apartment)
        {
             _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteApartment(Apartment apartment)
        {
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
        }
        public async Task<Apartment> GetApartmentById(int apartmentId)
        {
            var apartment = await _context.Apartments
                .FirstOrDefaultAsync(x => x.Id == apartmentId);

            return apartment;
        }

        public async Task<Apartment> GetApartmentWithRoomsById(int apartmentId)
        {
            var apartment = await _context.Apartments
                .Include(a => a.Rooms)
                .FirstOrDefaultAsync(a => a.Id == apartmentId);

            return apartment;
        }
        public async Task<IEnumerable<Apartment>> GetAllApartments()
        {
            var apartment = await _context.Apartments
                .ToListAsync();

            return apartment;
        }
    }
}
