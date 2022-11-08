using FluentAssertions;
using Newtonsoft.Json;
using PracticalTest.Endpoint.Common;
using PracticalTest.Infrastructure;
using PracticalTest.Write.Tests.Integration.Common;
using Xunit;

namespace PracticalTest.Write.Tests.Integration.Authenthication;

public class AuthenticateControllerTest:IClassFixture<IntegrationTestFactory<IIntegrationTest,PracticalTestWriteDbContext>>
{
    
    private readonly IntegrationTestFactory<IIntegrationTest, PracticalTestWriteDbContext> _factory;

    public AuthenticateControllerTest(IntegrationTestFactory<IIntegrationTest, PracticalTestWriteDbContext> factory)
    {
        _factory = factory;
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

        var client = _factory.CreateClient();
        var response = await client.PostAsync("/Auth/AccessToken",httpContent);
        var token =JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
        token.AccessToken.Should().NotBeNull();
        response.EnsureSuccessStatusCode();

    }
}