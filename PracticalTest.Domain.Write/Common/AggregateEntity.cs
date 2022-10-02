using PracticalTest.Domain.Write.Common.Mediator;

namespace PracticalTest.Domain.Write.Common;

public abstract class AggregateEntity:EntityBase,IAggregateEntity
{
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public void ClearEvents()
    {
        _domainEvents?.Clear();
    }
    protected void AddEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
}

public interface IAggregateEntity
{

}