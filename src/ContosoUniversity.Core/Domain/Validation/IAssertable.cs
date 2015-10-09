namespace ContosoUniversity.Core.Domain.Validation
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public interface IAssertable
    {
        ValidationMessageCollection Assert(params object[] dependentServices);
    }
}
