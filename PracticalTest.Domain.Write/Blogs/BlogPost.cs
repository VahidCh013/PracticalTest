using CSharpFunctionalExtensions;
using PracticalTest.Domain.Write.Common;

namespace PracticalTest.Domain.Write.Blogs;

public class BlogPost:EntityBase,ITimeAudit
{
    public string Summary  { get; }
    public string Body { get; }

    private BlogPost(string summary, string body)
    {
        Summary = summary;
        Body = body;
    }

    public DateTimeOffset CreatedOn { get; }
    public string CreatedBy { get; }
    public DateTimeOffset ModifiedOn { get; }
    public string ModifiedBy { get; }
}