namespace ContosoUniversity.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ContosoUniversityException : Exception
    {
        public ContosoUniversityException()
        {
        }

        public ContosoUniversityException(string message)
            : base(message)
        {
        }

        public ContosoUniversityException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ContosoUniversityException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
