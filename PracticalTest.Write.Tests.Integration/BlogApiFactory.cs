using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PracticalTest.Endpoint.Common;
using PracticalTest.Infrastructure;
using Xunit;

namespace PracticalTest.Write.Tests.Integration;

public class BlogApiFactory : WebApplicationFactory<IIntegrationTest>,IAsyncLifetime
{
    private readonly TestcontainerDatabase _container;

    public BlogApiFactory()
    {
        _container = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration
            {
                Database = "test_db",
                Username = "sa",
                Password = "Dock#rEx@mple"
            })
            .WithCleanUp(true)
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(PracticalTestWriteDbContext));
            services.AddDbContextFactory<PracticalTestWriteDbContext>(opt =>
                opt.UseSqlServer(_container.ConnectionString));
        });
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}