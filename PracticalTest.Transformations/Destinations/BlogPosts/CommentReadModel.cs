namespace PracticalTest.Transformations.Destinations.BlogPosts;

public class CommentReadModel
{
    public long Id { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public string Email { get; set; }
    public long BlogPostId { get; set; }
    public string BlogPostName { get; set; }
}