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
    
    public virtual BlogPost BlogPost { get; }

    private Comment(User user, BlogPost blogPost,string content)
    {
        User = user;
        BlogPost = blogPost;
        Content = content;
    }

    private Comment()
    {
        
    }
}