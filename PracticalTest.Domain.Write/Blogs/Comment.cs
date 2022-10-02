using CSharpFunctionalExtensions;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.Users;

namespace PracticalTest.Domain.Write.Blogs;

public class Comment:EntityBase,ITimeAudit
{
    public DateTimeOffset CreatedOn { get; }
    public string CreatedBy { get; }
    public DateTimeOffset ModifiedOn { get; }
    public string ModifiedBy { get; }


    public string Content { get; }
    public virtual User User { get; }
    

    private Comment(string content,User user)
    {
        User = user;
        Content = content;
    }

    protected Comment()
    {
        
    }
    
    public static Result<Comment> Create(string content,User user)
        =>Result.FailureIf( string.IsNullOrEmpty( content ), content, "Content name must not be an empty string or whitespace" )
            .Map( dt => new Comment(   content ,user) );
}