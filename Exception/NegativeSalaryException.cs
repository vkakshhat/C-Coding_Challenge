using System;

namespace ExceptionLibrary
{

    using System;

    public class NegativeSalaryException : Exception
    {
        public NegativeSalaryException(string message) : base(message) { }
    }

}
