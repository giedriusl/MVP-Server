using System;

namespace MVP.Entities.Exceptions
{
    public class ApartmentException : Exception
    {
        public string ErrorCode { get; set; }
        public Exception Exception { get; set; }
        public ApartmentException()
        {
        }

        public ApartmentException(Exception exception, string message) : base(message)
        {
            Exception = exception;
        }
        public ApartmentException(string message) : base(message)
        {
        }

        public ApartmentException(string message, string errorCode, Exception exception) : base(message)
        {
            ErrorCode = errorCode;
            Exception = exception;
        }
    }
}
