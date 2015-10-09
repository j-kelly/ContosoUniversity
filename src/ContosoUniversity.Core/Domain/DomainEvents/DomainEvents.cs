namespace ContosoUniversity.Core.Domain.DomainEvents
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class DomainEvents
    {
        private static IDomainEventHandler eventHandler = new BlankDomainEventHandler();

        public static void SetDomainEventHandler(IDomainEventHandler newHandler)
        {
            eventHandler = newHandler;
        }

        public static void Raise<T>(T domainEvent) where T : class, IDomainEvent
        {
            eventHandler.Handle(domainEvent);
        }
    }
}
