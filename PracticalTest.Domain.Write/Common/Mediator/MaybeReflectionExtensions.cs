using System.Reflection;
using CSharpFunctionalExtensions;

namespace PracticalTest.Domain.Write.Common.Mediator;

public static class MaybeReflectionExtensions
{
    public static Maybe<FieldInfo> TryFindField(this Type? type, string fieldName)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
#pragma warning disable 8604
        var field = Maybe<FieldInfo>.From(type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy));
        if (field.HasValue) return field;
        type = type.BaseType;
        while (type is not null)
        {
            field = Maybe<FieldInfo>.From(type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy));
            if (field.HasValue) return field;
            type = type.BaseType;
        }

#pragma warning restore 8604
        return Maybe<FieldInfo>.None;
    }
}