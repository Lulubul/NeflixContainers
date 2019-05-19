using System;

namespace Identity.Domain.Exceptions
{
    public class RegisterDomainException : Exception
    {
        public RegisterDomainException()
        { }

        public RegisterDomainException(string message)
            : base(message)
        { }

        public RegisterDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
