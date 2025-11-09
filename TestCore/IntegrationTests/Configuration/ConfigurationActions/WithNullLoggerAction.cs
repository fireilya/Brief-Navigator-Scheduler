using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TestCore.IntegrationTests.Configuration.ConfigurationActions;

public class WithNullLoggerAction : IConfigurationAction
{
    public ConfigurationActionType Type => ConfigurationActionType.WithNullLogger;
    public ConfigurationActionType[] ActionTypesToExclude { get; } = [ConfigurationActionType.WithRealLogger];

    public void Invoke(ConfigurationActionContext context)
    {
        context.ServiceCollection
           .AddSingleton<ILogger, NullLogger>()
           .AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
    }
}