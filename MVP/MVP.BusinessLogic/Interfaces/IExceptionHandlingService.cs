using Microsoft.EntityFrameworkCore;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IExceptionHandlingService
    {
        byte[] HandleConcurrencyException(DbUpdateConcurrencyException exception);
    }
}
