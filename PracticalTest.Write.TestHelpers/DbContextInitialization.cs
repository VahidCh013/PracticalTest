using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace PracticalTest.Write.TestHelpers
{
    public static class DbContextInitialization
    {
        public static DbContextOptionsBuilder<T> GetDbOptionsBuilder<T>(string connectionString, bool enableLazyLoading = false, bool inMemory = false, Guid? databaseId = null) where T : DbContext
        {
            var optionBuilder = new DbContextOptionsBuilder<T>();
            if (databaseId is null)
                databaseId = Guid.NewGuid();
            if (inMemory)
            {
                optionBuilder
                    .UseInMemoryDatabase($"pt-{databaseId:N}")
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
            else
            {
                optionBuilder
                    .UseSqlServer(connectionString,
                        b =>
                        {
                            b.MigrationsAssembly("PracticalTest.Domain.Write");
                            b.MigrationsHistoryTable("__PTWriteContextMigrationHistory",
                                "dbo");
                          
                        })
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }

            optionBuilder.UseLazyLoadingProxies();

            // if (mediator is null)
            // {
            //     var mock = new Mock<TestMediatorService>(MockBehavior.Loose, new Mock<IMediator>(MockBehavior.Loose).Object);
            //     mediator = mock.Object;
            // }


            return optionBuilder;
        }

        public static DbContextOptionsBuilder<T> GetInMemoryDbOptionsBuilder<T>(IConfiguration configuration = null, bool enableLazyLoading = true, Guid? databaseId = null)
            where T : DbContext
        {
            configuration ??= ConfigHelper.GetConfig();

            return GetDbOptionsBuilder<T>(configuration.GetConnectionString("PracticalTestConnection"), enableLazyLoading, true, databaseId: databaseId);
        }

        public static IDbContextFactory<T> GetDbContextFactory<T>(DbContextOptions<T> options) where T : DbContext
        {
            return new TestDbContextFactory<T>(options);
        }
    }

    public class TestDbContextFactory<T> : IDbContextFactory<T> where T : DbContext
    {
        private readonly DbContextOptions<T> _options;

        public TestDbContextFactory(DbContextOptions<T> options)
        {
            _options = options;
        }

        public T CreateDbContext()
        {
            return (T) Activator.CreateInstance(typeof(T), _options);
        }
    }
}