using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestCore.IntegrationTests.Configuration.ConfigurationActions;

public class WithRealLoggerAction : IConfigurationAction
{
    public ConfigurationActionType Type => ConfigurationActionType.WithRealLogger;
    public ConfigurationActionType[] ActionTypesToExclude { get; } = [ConfigurationActionType.WithNullLogger];

    public void Invoke(ConfigurationActionContext context)
    {
        context.ServiceCollection.AddLogging(x => x.AddConsole());
    }
}