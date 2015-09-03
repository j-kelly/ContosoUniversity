namespace ContosoUniversity.Core.Domain.ContextualValidation
{
    public interface IDomainValidatable<TCommandModel> where TCommandModel : class
    {
        TCommandModel CommandModel { get; }

        IContextualValidation ContextualValidation { get; }

        ValidationMessageCollection Validate(params object[] dependentServices);
    }
}
