namespace ContosoUniversity.Core.Domain.DomainEvents
{
    public interface ISubscribe<in T> where T : class, ContosoUniversity.Core.Domain.DomainEvents.IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
