namespace ContosoUniversity.Core.Domain.DomainEvents
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class BlankDomainEventHandler : IDomainEventHandler
    {
        public void Handle<T>(T domainEvent) where T : class, IDomainEvent
        {
        }
    }
}
