namespace PracticalTest.Domain.Write.Common.Mediator;

public interface ICreatedEvent<in TAggregateEntity, in TEvent> where TAggregateEntity : class, IAggregateEntity where TEvent : new()

{
   
        
    /// <summary>
    ///     This method will be called after the entity was stored to the database successfully.
    ///     So <paramref name="entity" /> will then contain the actual database keys.
    /// </summary>
    /// <param name="entity">The aggregate entity</param>
    void CreateFromEntity(TAggregateEntity entity);
}