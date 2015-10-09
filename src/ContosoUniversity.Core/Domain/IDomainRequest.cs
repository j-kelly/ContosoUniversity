namespace ContosoUniversity.Core.Domain
{
    using Validation;

    public interface IDomainRequest : IAssertable
    {
        ICommandModel CommandModel { get; }
    }
}
