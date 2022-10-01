using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PracticalTest.Infrastructure;

public static class StartupExtension
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<PracticalTestWriteDbContext>(    (provider, options) => SetUpWriteContext(configuration,  options, provider)
        );
        return services;
    }

    private static void SetUpWriteContext(IConfiguration configuration, DbContextOptionsBuilder options,
        IServiceProvider provider)
    {
        options
            .UseSqlServer(configuration.GetConnectionString("PracticalTestConnection"),
                b =>
                {
                    b.MigrationsAssembly("PracticalTest.Infrastructure.Write");
                    b.MigrationsHistoryTable("__PTWriteContextMigrationHistory",
                        "dbo");
                })
            .UseLazyLoadingProxies()
            .EnableDetailedErrors();

        //This options only should be enabled in Development environment
        //IHostEnvironment should be included in production applications.
        options.EnableSensitiveDataLogging();
    }
}