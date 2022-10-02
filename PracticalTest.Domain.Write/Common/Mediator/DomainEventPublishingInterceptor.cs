using MediatR;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace PracticalTest.Domain.Write.Common.Mediator;

public class DomainEventPublishingInterceptor:SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public DomainEventPublishingInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

   
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var changedStates = new[] {EntityState.Added, EntityState.Deleted, EntityState.Modified};

        var entities = eventData.Context.ChangeTracker.Entries().Where( e => e.Entity is ITimeAudit && changedStates.Contains( e.State ) );
        foreach ( var entity in entities ) {
            UpdateTimeAudit( entity.Entity );
        }
        return base.SavingChanges(eventData, result);
    }
    private void UpdateTimeAudit( object entity )
    {
        var setMethod = entity.GetType().GetProperty( nameof(ITimeAudit.ModifiedOn) )?.GetSetMethod( nonPublic: true );
        setMethod?.Invoke( entity, new object[] { DateTimeOffset.UtcNow } );
    }
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        var changedStates = new[] {EntityState.Added, EntityState.Deleted, EntityState.Modified};
        var entities = eventData.Context.ChangeTracker.Entries().Where( e => e.Entity is ITimeAudit && changedStates.Contains( e.State ) );
        foreach ( var entity in entities ) {
            UpdateTimeAudit( entity.Entity );
        }
        return result;
    }
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = new())
    { 
        var changedStates = new[] {EntityState.Added, EntityState.Deleted, EntityState.Modified};
        foreach (var entityEntry in eventData.Context.ChangeTracker.Entries<IAggregateEntity>()) PublishEvents(entityEntry.Entity);
        var entities = eventData.Context.ChangeTracker.Entries().Where( e => e.Entity is ITimeAudit && changedStates.Contains( e.State ) );
        foreach ( var entity in entities ) {
            UpdateTimeAudit( entity.Entity );
        }
        return new ValueTask<int>(result);
    }
    private void PublishEvents(IAggregateEntity entity)
    {
        var events = GetDomainEvents(entity);

        try
        {
            foreach (var notification in events)
            {
                var iCreatedEvent = notification.GetType().GetInterfaces().FirstOrDefault(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICreatedEvent<,>));
                if (iCreatedEvent != null)
                {
                    var method = notification.GetType().GetMethod("CreateFromEntity");
                    method?.Invoke(notification, new[] {entity});
                }
                _mediator.Publish(notification);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private IEnumerable<IDomainEvent> GetDomainEvents(IAggregateEntity entity)
    {
        var fieldInfo = entity.GetType().TryFindField("_domainEvents");
        if (fieldInfo.HasNoValue) return new List<IDomainEvent>();
        var fieldValue = fieldInfo.Value.GetValue(entity);
        return fieldValue is IEnumerable<IDomainEvent> domainEvents ? domainEvents : new List<IDomainEvent>();
    }
}