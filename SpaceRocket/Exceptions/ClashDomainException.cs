using System;

namespace SpaceRocket.Domain.Exceptions
{
    public class ClashDomainException : Exception
    {
        public ClashDomainException()
        {
        }

        public ClashDomainException(string message) : base(message)
        {
        }

        public ClashDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
