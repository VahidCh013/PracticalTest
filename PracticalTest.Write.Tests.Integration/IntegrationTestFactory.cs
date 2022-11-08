using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace PracticalTest.Write.Tests.Integration;

public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class where TDbContext : DbContext
{
    private readonly TestcontainerDatabase _container;

    public IntegrationTestFactory()
    {
        _container = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration
            {
                Password = "P@ssw0rd"
            })
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove AppDbContext
            services.RemoveDbContext<TDbContext>();
            
            // Add DB context pointing to test container
            services.AddDbContextFactory<TDbContext>(options => { options.UseSqlServer(_container.ConnectionString); });
            // Ensure schema gets created
            //services.EnsureDbCreated<TDbContext>();
        });
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));
        if (descriptor != null) services.Remove(descriptor);
    }

    // If scope is not created in Program.cs then uncomment following method
    // public static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
    // {
    //     var serviceProvider = services.BuildServiceProvider();
    //
    //     using var scope = serviceProvider.CreateScope();
    //     var scopedServices = scope.ServiceProvider;
    //     var context = scopedServices.GetRequiredService<T>();
    //     context.Database.EnsureCreated();
    // }
}

