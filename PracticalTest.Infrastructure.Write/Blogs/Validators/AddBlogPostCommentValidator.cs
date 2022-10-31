using FluentValidation;
using PracticalTest.Infrastructure.Blogs.Commands;

namespace PracticalTest.Infrastructure.Blogs.Validators;

public class AddBlogCommentValidator:AbstractValidator<AddBlogCommand>
{
    public AddBlogCommentValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.content).NotEmpty();
        RuleFor(x => x.email).NotEmpty();
        RuleFor(x => x.tags).NotEmpty();
    }
}