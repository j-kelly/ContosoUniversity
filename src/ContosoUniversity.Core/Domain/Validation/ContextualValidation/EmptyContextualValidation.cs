namespace ContosoUniversity.Core.Domain.ContextualValidation
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class EmptyContextualValidation : IContextualValidation
    {
        public ValidationMessageCollection Validate(params object[] dependentServices)
        {
            return new ValidationMessageCollection();
        }
    }
}
