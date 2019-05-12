using System;

namespace MVP.Entities.Exceptions
{
    public class ApartmentException : Exception
    {
        public string ErrorCode { get; set; }

        public ApartmentException()
        {
        }

        public ApartmentException(string message) : base(message)
        {
        }

        public ApartmentException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
