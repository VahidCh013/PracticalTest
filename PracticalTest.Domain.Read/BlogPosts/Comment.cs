namespace PracticalTest.Domain.Read.BlogPosts;

public class Comment
{
    public long Id { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public string Email { get; set; }
    public long BlogPostId { get; set; }
    public string BlogPostName { get; set; }
}