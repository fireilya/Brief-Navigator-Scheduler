using System;
using Microsoft.Extensions.DependencyInjection;

namespace TestCore.IntegrationTests.Configuration.ConfigurationActions;

public class CustomizeServiceCollectionAction(
    Action<IServiceCollection> customize
) : IConfigurationAction
{
    public ConfigurationActionType Type => ConfigurationActionType.CustomizeServiceCollection;
    public ConfigurationActionType[] ActionTypesToExclude { get; } = [];

    public void Invoke(ConfigurationActionContext context)
    {
        customize(context.ServiceCollection);
    }
}