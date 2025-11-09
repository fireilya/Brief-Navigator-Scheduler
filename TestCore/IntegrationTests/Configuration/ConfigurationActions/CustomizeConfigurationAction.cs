using System;
using Microsoft.Extensions.Configuration;

namespace TestCore.IntegrationTests.Configuration.ConfigurationActions;

public class CustomizeConfigurationAction(
    Action<IConfigurationManager> customize
) : IConfigurationAction
{
    public ConfigurationActionType Type => ConfigurationActionType.CustomizeConfiguration;
    public ConfigurationActionType[] ActionTypesToExclude { get; } = [];

    public void Invoke(ConfigurationActionContext context)
    {
        customize(context.ConfigurationManager);
    }
}