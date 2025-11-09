using Core.EFCore.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDao(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddNpg().AddSingleton<IDbContextModelConfigurator, DbContextModelConfigurator>();
        return serviceCollection;
    }
}