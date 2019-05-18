using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IFileReader
    {
        Task ReadApartmentCalendarFile(int apartmentId, IFormFile file);
        Task<List<Calendar>> ReadUsersCalendarFile(IFormFile file);
    }
}
