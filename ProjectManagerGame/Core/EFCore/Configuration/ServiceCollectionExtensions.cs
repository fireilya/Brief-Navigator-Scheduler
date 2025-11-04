using Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.EFCore.Configuration;

public static class ServiceCollectionExtensions
{
    private const int PoolSize = 128;

    public static IServiceCollection AddNpg(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDbContextOptionsConfigurator, NpgDbContextOptionsConfigurator>()
           .AddOptionsWithValidation<DataContextOptions>()
           .AddDbContextPool<DataContext>(
                (provider, optionsBuilder) =>
                    provider.GetRequiredService<IDbContextOptionsConfigurator>().Configure(optionsBuilder),
                PoolSize
            )
           .AddPooledDbContextFactory<DataContext>(
                (provider, optionsBuilder) =>
                    provider.GetRequiredService<IDbContextOptionsConfigurator>().Configure(optionsBuilder),
                PoolSize
            )
           .AddSingleton<IDataContextFactory, DataContextFactory>()
           .AddScoped<IDataContext>(x => x.GetRequiredService<DataContext>());

        return serviceCollection;
    }
}