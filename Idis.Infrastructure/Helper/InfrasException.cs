using System;

namespace Idis.Infrastructure
{
    public class InfrasException : Exception
    {
        internal InfrasException(string businessMessage)
               : base(businessMessage)
        {
        }

        internal InfrasException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}