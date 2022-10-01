using CSharpFunctionalExtensions;

namespace PracticalTest.Domain.Write.Common;

public static class ErrorCodeExtensions
{
    public static string WithMessage(this ErrorCode code, string message)
    {
        return $"{Enum.GetName(typeof(ErrorCode), code)}|{message}";
    }
    public static (ErrorCode code, string message) ParseFromMessage(this string message)
    {
        var parts = message.Split("|").ToList();
        if (Enum.TryParse(typeof(ErrorCode), parts.First(), true, out var code))
            return ((ErrorCode code, string message)) (code, parts.Skip(1).FirstOrDefault() ?? string.Empty);

        return (ErrorCode.Unknown, message);
    }

    public static (string code, string message) ParseFromMessageAsStrings(this string message)
    {
        var result = ParseFromMessage(message);
        return (Enum.GetName(typeof(ErrorCode), result.code), result.message);
    }

    public static IEnumerable<UserError> UserErrorFromMessageString(this string message)
    {
        var messages = message.Split(Result.ErrorMessagesSeparator);
        var results = messages.Select(ParseFromMessage);
        return results.Select(result =>
            new UserError(result.message, Enum.GetName(typeof(ErrorCode), result.code)));
    }
}