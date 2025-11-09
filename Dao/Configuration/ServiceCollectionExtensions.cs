using Core.EFCore.Configuration;
using Dao.DataBase;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDao(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDbContextModelConfigurator, DbContextModelConfigurator>()
           .AddTransient<ITestRepository, TestRepository>();

        return serviceCollection;
    }
}