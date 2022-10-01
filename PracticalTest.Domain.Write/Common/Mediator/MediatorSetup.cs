using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PracticalTest.Domain.Write.Common.Mediator;

public static class MediatorSetup
{
    public static IServiceCollection AddMediator(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMediatR(assemblies);
        //services.AddTransient<IMediatorService,MediatorService>();
        return services;
    }
}