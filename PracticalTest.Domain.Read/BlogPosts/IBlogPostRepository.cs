namespace PracticalTest.Domain.Read.BlogPosts;

public interface IBlogPostRepository
{
    Task<List<BlogPost>> GetAllBlogPosts(string email);
    Task<List<BlogPost>> GetTenDaysBlogPosts();
}