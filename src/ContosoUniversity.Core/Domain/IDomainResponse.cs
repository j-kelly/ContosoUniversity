namespace ContosoUniversity.Core.Domain
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public interface IDomainResponse
    {
        ValidationMessageCollection ValidationDetails { get; }
        bool HasValidationIssues { get; }
    }
}
