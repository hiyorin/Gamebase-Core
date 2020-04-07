using System;

namespace Gamebase.Application.Loader
{
    public enum ErrorStatus
    {
        Offline,
        Timeout,
        Fatal,
    }

    public enum ErrorResult
    {
        Retry,
        Error,
    }
    
    public sealed class ErrorMessage
    {
        public readonly ErrorStatus Status;

        public readonly string Message;

        public readonly Exception Exception;

        private ErrorMessage(ErrorStatus status, string message, Exception exception)
        {
            Status = status;
            Message = message;
            Exception = exception;
        }

        public override string ToString()
        {
            return "";
        }
        
        public static ErrorMessage Create(ErrorStatus type, string message)
        {
            return new ErrorMessage(type, message, null);
        }

        public static ErrorMessage Create(ErrorStatus type, Exception exception)
        {
            return new ErrorMessage(type, null, exception);
        }
    }
}