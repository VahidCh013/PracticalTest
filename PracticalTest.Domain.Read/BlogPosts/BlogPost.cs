namespace PracticalTest.Domain.Read.BlogPosts;

public class BlogPost
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Desciption { get; set; }
    public string UserEmail { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public string Tags { get; set; }
    public List<Comment> Comments { get; set; }
}