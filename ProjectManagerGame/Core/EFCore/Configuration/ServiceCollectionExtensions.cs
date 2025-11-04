using Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.EFCore.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNpg(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDbContextOptionsConfigurator, NpgDbContextOptionsConfigurator>();
        serviceCollection.AddOptionsWithValidation<DataContextOptions>()
           .AddDbContextPool<DataContext>((provider, optionsBuilder) =>
                provider.GetRequiredService<IDbContextOptionsConfigurator>().Configure(optionsBuilder)
            );

        return serviceCollection;
    }
}