namespace ContosoUniversity.Core.Domain.DomainEvents
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class DomainEvents
    {
        private static ContosoUniversity.Core.Domain.DomainEvents.IDomainEventHandler eventHandler = new ContosoUniversity.Core.Domain.DomainEvents.BlankDomainEventHandler();

        public static void SetDomainEventHandler(ContosoUniversity.Core.Domain.DomainEvents.IDomainEventHandler newHandler)
        {
            eventHandler = newHandler;
        }

        public static void Raise<T>(T domainEvent) where T : class, ContosoUniversity.Core.Domain.DomainEvents.IDomainEvent
        {
            eventHandler.Handle(domainEvent);
        }
    }
}
