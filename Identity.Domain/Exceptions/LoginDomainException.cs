using System;

namespace Identity.Domain.Exceptions
{
    public class LoginDomainException : Exception
    {
        public LoginDomainException()
        { }

        public LoginDomainException(string message)
            : base(message)
        { }

        public LoginDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
