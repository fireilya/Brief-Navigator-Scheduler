using System.Collections.Generic;
using Core.EFCore.Configuration;
using Microsoft.Extensions.Configuration;

namespace TestCore.IntegrationTests.Configuration.ConfigurationActions;

public class WithPostgresDataBaseAction : IConfigurationAction
{
    public ConfigurationActionType Type => ConfigurationActionType.WithDataBase;
    public ConfigurationActionType[] ActionTypesToExclude { get; } = [];

    public void Invoke(ConfigurationActionContext context)
    {
        context.TestContainerBuilder.WithPostgres();
        var configurationDictionary = new Dictionary<string, string?>
        {
            ["DataBaseOptions:ConnectionStringTemplate"] = context.TestContainerBuilder.ConnectionStringTemplate,
            ["DataBaseOptions:Username"] = context.TestContainerBuilder.Username,
            ["DataBaseOptions:Password"] = context.TestContainerBuilder.Password,
            ["DataBaseOptions:EnableSensitiveDataLogging"] = "true",
        };
        context.ConfigurationManager.AddInMemoryCollection(configurationDictionary);
        context.ServiceCollection.AddNpg();
    }
}