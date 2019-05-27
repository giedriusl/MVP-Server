using MVP.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface IApartmentRepository
    {
        Task<Apartment> AddApartmentAsync(Apartment apartment);
        Task UpdateApartmentAsync(Apartment apartment);
        Task<IEnumerable<Apartment>> GetAllApartmentsAsync();
        Task<Apartment> GetApartmentByIdAsync(int apartmentId);
        Task<Apartment> GetApartmentWithRoomsByIdAsync(int apartmentId);
        Task DeleteApartmentAsync(Apartment apartment);
        Task<List<ApartmentRoom>> GetApartmentRoomsByNumberAsync(int apartmentId, List<int> roomNumbers);
        Task<IEnumerable<ApartmentRoom>> GetRoomsByApartmentIdAsync(int apartmentId);
        Task<IEnumerable<ApartmentRoom>> GetRoomsByApartmentIdAndDateAsync(int apartmentId, DateTimeOffset start, DateTimeOffset end);
        Task<bool> IsRoomAvailable(int apartmentId, int roomId, DateTimeOffset start, DateTimeOffset end);
    }
}
