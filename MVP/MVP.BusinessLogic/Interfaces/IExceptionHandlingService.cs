using Microsoft.EntityFrameworkCore;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IExceptionHandlingService
    {
        byte[] HandleConcurrencyExceptionAsync(DbUpdateConcurrencyException exception);
    }
}
