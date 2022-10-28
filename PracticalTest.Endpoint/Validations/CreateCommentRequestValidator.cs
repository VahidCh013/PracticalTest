using FluentValidation;
using PracticalTest.Domain.Write.Users;

namespace PracticalTest.Endpoint.Validations;

public class CreateCommentRequestValidator:AbstractValidator<CreateCommentRequest>
{
    public CreateCommentRequestValidator()
    {
        RuleFor(x => x.Comment).Length(1,100);
        RuleFor(x => x.BlogpostId).NotEmpty();
    }
}