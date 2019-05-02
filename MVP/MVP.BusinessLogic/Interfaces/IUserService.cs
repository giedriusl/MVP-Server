using System.Threading.Tasks;
using MVP.Entities.Entities;
using MVP.Entities.Models;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<NewUserDto> CreateAsync(NewUserDto newUserDto);
    }
}
