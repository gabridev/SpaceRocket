using System;

namespace SpaceRocket.Domain.Exceptions
{
    public class OutOfLandingAreaDomainException : Exception
    {
        public OutOfLandingAreaDomainException()
        {
        }

        public OutOfLandingAreaDomainException(string message) : base(message)
        {
        }

        public OutOfLandingAreaDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
