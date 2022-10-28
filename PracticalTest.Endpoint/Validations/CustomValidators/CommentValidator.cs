using FluentValidation;

namespace PracticalTest.Endpoint.Validations.CustomValidators;

public static class CommentValidator
{
    public static IRuleBuilderOptions<T, string> CommentLenght<T>(this IRuleBuilder<T, string> ruleBuilder,int length)
    {
        return ruleBuilder.Must((objectRoot,comment,context) =>
            {
                context.MessageFormatter.AppendArgument("PropertyLength", comment.Length);
                context.MessageFormatter.AppendArgument("ActualLength", length);
                return comment.Length > length;
            })
            .WithMessage("{PropertyName} must not be less than {ActualLength} characters. You provided '{PropertyValue}' which has {PropertyLength} characters.");
    }
}