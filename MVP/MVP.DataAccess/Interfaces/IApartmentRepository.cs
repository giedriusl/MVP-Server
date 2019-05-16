﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    public interface IApartmentRepository
    {
        Task<Apartment> AddApartment(Apartment apartment);
        Task UpdateApartment(Apartment apartment);
        Task<IEnumerable<Apartment>> GetAllApartments();
        Task<Apartment> GetApartmentById(int apartmentId);
        Task<Apartment> GetApartmentWithRoomsById(int apartmentId);
        Task DeleteApartment(Apartment apartment);
        Task<List<ApartmentRoom>> GetApartmentRoomsByNumber(int apartmentId, List<int> roomNumbers);
        Task<List<ApartmentRoom>> GetRoomsByApartmentId(int apartmentId);
    }
}