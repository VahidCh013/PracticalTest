namespace PracticalTest.Domain.Write.Common.Mediator;

public abstract record Payload(IEnumerable<UserError>? Errors)
{
    public bool HasErrors => !Errors.IsNullOrEmpty();
}