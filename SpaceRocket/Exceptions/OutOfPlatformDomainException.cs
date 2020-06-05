using System;

namespace SpaceRocket.Domain.Exceptions
{
    public class OutOfPlatformDomainException : Exception
    {
        public OutOfPlatformDomainException()
        {
        }

        public OutOfPlatformDomainException(string message) : base(message)
        {
        }

        public OutOfPlatformDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
