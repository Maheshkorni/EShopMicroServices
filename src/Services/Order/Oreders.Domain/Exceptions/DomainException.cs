using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message):base($"DomainException : \"{message}\" throws from domain layer.") { }
    }
}
