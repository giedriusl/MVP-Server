using System;

namespace MVP.Entities.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public string ErrorCode { get; set; }
        public Exception Exception { get; set; }
        public BusinessLogicException()
        {
        }

        public BusinessLogicException(Exception exception, string message) : base(message)
        {
            Exception = exception;
        }
        public BusinessLogicException(string message) : base(message)
        {
        }

        public BusinessLogicException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
