using CSharpFunctionalExtensions;
using PracticalTest.Domain.Write.Common;

namespace PracticalTest.Domain.Write.ValueObjects;

public class Name:ValueObject
{
    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; }
    
    public static Result<Name> Create(string modelCode) =>
        Result
            .FailureIf(string.IsNullOrWhiteSpace(modelCode), modelCode,
                ErrorCode.MustNotBeEmpty.WithMessage("Title must not be an empty string or whitespace"))
            .Ensure(mc => mc.Length <= 100,
                ErrorCode.MaxLength.WithMessage("Title most not be longer than 100 characters."))
            .Map(mc => new Name(mc));
    
    
    public override string ToString()
    {
        return Value;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}