using FluentValidation;
using PracticalTest.Domain.Write.Users;
using PracticalTest.Endpoint.Models;
using PracticalTest.Endpoint.Validations.CustomValidators;

namespace PracticalTest.Endpoint.Validations;

public class CreateCommentRequestValidator:AbstractValidator<CreateCommentRequest>
{
    public CreateCommentRequestValidator()
    {
         RuleFor(x => x.Comment)
             .NotEmpty()
             .WithMessage("Comment must not be empty");
        RuleFor(x => x.Comment)
            .CommentLenght(5);
        RuleFor(x => x.Comment).Length(1,100);
        RuleFor(x => x.BlogPostId).NotEmpty();
    }
}