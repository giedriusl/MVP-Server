using Microsoft.EntityFrameworkCore;
using MVP.BusinessLogic.Interfaces;
using System.Linq;

namespace MVP.BusinessLogic.Services
{
    public class ExceptionHandlingService : IExceptionHandlingService
    {
        public byte[] HandleConcurrencyException(DbUpdateConcurrencyException exception)
        {
            var entry = exception.Entries.First();
            var databaseValues = entry.GetDatabaseValues();
            var timeStamp = databaseValues["Timestamp"];

            return (byte[]) timeStamp;
        }
    }
}
