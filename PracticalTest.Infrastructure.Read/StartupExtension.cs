using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PracticalTest.Infrastructure.Read;

public static class StartupExtension
{
    public static IServiceCollection AddReadDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<PracticalTestReadDbContext>(    (provider, options) => SetUpWriteContext(configuration,  options, provider)
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
                    b.MigrationsAssembly("PracticalTest.Infrastructure.Read");
                    b.MigrationsHistoryTable("__PTReadContextMigrationHistory",
                        "read");
                })
            .EnableDetailedErrors();

        //This option only should be enabled in Development environment
        //IHostEnvironment should be included in production applications.
        options.EnableSensitiveDataLogging();
    }
}