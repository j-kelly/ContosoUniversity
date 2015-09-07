namespace ContosoUniversity.Core.Domain.InvariantValidation
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class EmptyInvariantValidation : IInvariantValidation
    {
        public void StartAsserting(params object[] dependentServices)
        {
        }
    }
}
