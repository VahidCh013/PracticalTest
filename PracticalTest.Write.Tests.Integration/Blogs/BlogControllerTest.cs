using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using PracticalTest.Write.Tests.Integration.Common;
using Xunit;

namespace PracticalTest.Write.Tests.Integration.Blogs;

[Collection("Test collection")]
public class BlogControllerTest:IAsyncLifetime
{
    private readonly BlogApiFactory _factory;
    private readonly Func<Task> _resetDatabase;
    public BlogControllerTest(BlogApiFactory factory)
    {
        _factory = factory;
        _resetDatabase = factory.ResetDatabase;

    }

    [Theory]
    [InlineData("WorldCup2022","WorldCup2022description","content")]
    public async Task? Can_Create_Blog(string name,string description,string content)
    {
        //Arrange
        var credential = new
        {
            email = "test.user@gmail.com",
            password = "P@ssw0rd"
        };

        var json = JsonConvert.SerializeObject(credential);
        var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var client = _factory.HttpClient;
        var response = await client.PostAsync("/Auth/AccessToken",httpContent);
        var token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
       

        var tags = new [] { "Soccer" };
        var jsonTags = JsonConvert.SerializeObject(tags);
        var httpContentTags = new StringContent(jsonTags, System.Text.Encoding.UTF8, "application/json");

        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{client.BaseAddress}api/Blog/AddBlog?name={name}&description={description}&content={content}"),
            Headers = { 
                { HttpRequestHeader.Authorization.ToString(), $"Bearer {token?.AccessToken}" },
                { HttpRequestHeader.Accept.ToString(), "application/json" },
                { "X-Version", "1" }
            },
            Content = httpContentTags
        };
       

        var addBlogResponse = client.SendAsync(httpRequestMessage).Result;
        var result = JsonConvert.DeserializeObject<Result>(await addBlogResponse.Content.ReadAsStringAsync());
        result?.Id.Should().NotBeNull();
        result?.Errors.Should().BeNull();
        addBlogResponse.EnsureSuccessStatusCode();

    }

 

    private record Result(string Id, List<Errors> Errors,bool HasErrors);

    public record Errors(string Code, string Message);

    public Task InitializeAsync()=>Task.CompletedTask;


    public Task DisposeAsync() => _resetDatabase();
}