using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticalTest.Domain.Write.Common.Mediator;

namespace PracticalTest.Infrastructure;

public static class StartupExtension
{
    public static IServiceCollection AddWriteDependencies(this IServiceCollection services, IConfiguration configuration)
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
        options.AddInterceptors(
            new DomainEventPublishingInterceptor(provider.GetRequiredService<IMediator>())
        );
        //This option only should be enabled in Development environment
        //IHostEnvironment should be included in production applications.
        options.EnableSensitiveDataLogging();
    }
}