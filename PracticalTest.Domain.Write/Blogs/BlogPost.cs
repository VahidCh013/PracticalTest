using CSharpFunctionalExtensions;
using PracticalTest.Domain.Write.Common;

namespace PracticalTest.Domain.Write.Blogs;

public class BlogPost:EntityBase,ITimeAudit
{

    private readonly List<Tag> _tags;
    private readonly List<Comment> _comments;
    public string Content  { get; }

    private BlogPost(string content,Blog blog)
    {
        Content = content;
        Blog = blog;
        _tags = new List<Tag>();
    }

    private BlogPost()
    {
        
    }
    public DateTimeOffset CreatedOn { get; }
    public string CreatedBy { get; }
    public DateTimeOffset ModifiedOn { get; }
    public string ModifiedBy { get; }
    
    public virtual Blog Blog { get; }

    public virtual List<Tag> Tags => _tags;
    public virtual List<Comment> Comments => _comments;

    public static Result<BlogPost> Create(string content,Blog blog)
    =>Result.FailureIf( string.IsNullOrEmpty( content ), content, "Content name must not be an empty string or whitespace" )
            .Map( dt => new BlogPost( dt, blog ) );
}