namespace ContosoUniversity.Core.Domain.InvariantValidation
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public class InvariantValidationException : ContosoUniversityException
    {
        public InvariantValidationException(string message)
            : base(message)
        {
        }

        public InvariantValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
