using System.Security.Cryptography;
using CSharpFunctionalExtensions;
using JetBrains.Annotations;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.Users;
using PracticalTest.Domain.Write.ValueObjects;

namespace PracticalTest.Domain.Write.Blogs;

public class BlogPost:AggregateEntity,IBlog,ITimeAudit
{
    public Name Name { get; private set; }
    public string Description { get;private set; }
    public virtual User User { get; }
    private readonly List<Tag> _tags;
    private readonly List<Comment> _comments;
    public string Content  { get; }

    private BlogPost(Name name,string description,User user,string content)
    {
        Content = content;
        _tags = new List<Tag>();
        Name = name;
        Description = description;
        User = user;
    }

    protected BlogPost()
    {
        
    }
    public DateTimeOffset CreatedOn { get; }
    public string CreatedBy { get; }
    public DateTimeOffset ModifiedOn { get; }
    public string ModifiedBy { get; }
    

    public virtual List<Tag> Tags => _tags;
    public virtual List<Comment> Comments => _comments;

    public static Result<BlogPost> Create(Name name,string description,User user,string content)
    =>Result.FailureIf( string.IsNullOrEmpty( content ), content, "Content name must not be an empty string or whitespace" )
            .Map( dt => new BlogPost(  name, description, user, content ) );

    public Result ApplyTags(List<Tag> tags)
    {
        Tags.Clear();
        _tags.AddRange(tags);
        return Result.Success();
    }

    public Result AddComment(Comment comment, User user)
    {
        Comments.Add(comment);
        return Result.Success();
    }

    public Result UpdateName(Name name)
    {
        Name = name;
        return Result.Success();
    }
    
    public Result UpdateDescription(string description)
    {
        Description = description;
        return Result.Success();
    }

}