using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IFileReader
    {
        Task ReadCalendarFile(int apartmentId, IFormFile file);
    }
}
