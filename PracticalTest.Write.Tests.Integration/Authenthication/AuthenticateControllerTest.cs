using System.Runtime.CompilerServices;
using FluentAssertions;
using Newtonsoft.Json;
using PracticalTest.Write.Tests.Integration.Common;
using Xunit;

namespace PracticalTest.Write.Tests.Integration.Authenthication;

[Collection("Test collection")]
public class AuthenticateControllerTest:IAsyncLifetime
{

    private readonly BlogApiFactory _factory;
    private readonly Func<Task> _resetDatabase;

    public AuthenticateControllerTest(BlogApiFactory factory)
    {
        _factory = factory;
        _resetDatabase = factory.ResetDatabase;
    }

    [Fact]
    public async Task Authenticate_Success()
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
        var token =JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
        token.AccessToken.Should().NotBeNull();
        response.EnsureSuccessStatusCode();

    }
    public Task InitializeAsync()=>Task.CompletedTask;
    public Task DisposeAsync() => _resetDatabase();
}