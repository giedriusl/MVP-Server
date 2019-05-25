using System;

namespace MVP.Entities.Exceptions
{
    public class FileReaderException : Exception
    {
        public string ErrorCode { get; set; }
        public Exception Exception { get; set; }

        public FileReaderException()
        {

        }

        public FileReaderException(Exception exception, string message) : base(message)
        {
            Exception = exception;
        }
        public FileReaderException(string message) : base(message)
        {
        }

        public FileReaderException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
