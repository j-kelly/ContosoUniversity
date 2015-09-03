namespace ContosoUniversity.Core.Domain.DomainEvents
{
    public interface IDomainEventHandler
    {
        void Handle<T>(T domainEvent) where T : class, ContosoUniversity.Core.Domain.DomainEvents.IDomainEvent;
    }
}
