using Microsoft.EntityFrameworkCore;
using System;

namespace Idis.Infrastructure
{
    public class InfrasException : DbUpdateException
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