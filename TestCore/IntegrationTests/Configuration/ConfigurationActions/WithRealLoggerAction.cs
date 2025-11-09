using Core.Logging.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestCore.IntegrationTests.Configuration.ConfigurationActions;

public class WithRealLoggerAction : IConfigurationAction
{
    public ConfigurationActionType Type => ConfigurationActionType.WithRealLogger;
    public ConfigurationActionType[] ActionTypesToExclude { get; } = [ConfigurationActionType.WithNullLogger];

    public void Invoke(ConfigurationActionContext context)
    {
        context.ServiceCollection
           .AddLogging(builder => builder.AddConsole())
           .AddCustomLogger(new ConfigurationManager(), "Development");
    }
}