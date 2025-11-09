using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptionsWithValidation<TOptions>(this IServiceCollection serviceCollection)
        where TOptions : class
    {
        var pathAttribute = typeof(TOptions).GetCustomAttribute<OptionsPathAttribute>();
        if (pathAttribute is null)
        {
            throw new ArgumentException($"У класса настроек должен быть атрибут {nameof(OptionsPathAttribute)}");
        }

        serviceCollection.AddOptions<TOptions>()
           .BindConfiguration(pathAttribute.OptionsPath)
           .ValidateDataAnnotations()
           .ValidateOnStart();

        return serviceCollection;
    }
}