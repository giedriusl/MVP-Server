using System;

namespace MVP.Entities.Exceptions
{
    public class InvalidUserException : Exception
    {
        public string ErrorCode { get; set; }

        public InvalidUserException()
        {
        }

        public InvalidUserException(string message) : base(message)
        {
        }

        public InvalidUserException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
