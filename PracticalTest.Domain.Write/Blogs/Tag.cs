using CSharpFunctionalExtensions;
using PracticalTest.Domain.Write.Common;

namespace PracticalTest.Domain.Write.Blogs;

public class Tag:EntityBase,ITimeAudit
{
    private readonly List<BlogPost> _blogPosts;
    public string Name { get;  }

    private Tag(string name)
    {
        Name = name;
        _blogPosts = new List<BlogPost>();
    }

    protected Tag()
    {
        
    }
    public virtual IList<BlogPost> BlogPosts => _blogPosts;


    public DateTimeOffset CreatedOn { get; }
    public string CreatedBy { get; }
    public DateTimeOffset ModifiedOn { get; }
    public string ModifiedBy { get; }


    public static Result<Tag> Create(string name)
      =>Result.FailureIf( string.IsNullOrEmpty( name ), name, "Tag name must not be an empty string or whitespace" )
    .Map( dt => new Tag(  name) );
}