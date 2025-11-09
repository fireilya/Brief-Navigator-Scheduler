using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestCore.IntegrationTests.Configuration.Containers;

namespace TestCore.IntegrationTests.Configuration.ConfigurationActions;

public record ConfigurationActionContext(
    IServiceCollection ServiceCollection,
    IConfigurationManager ConfigurationManager,
    ITestContainerBuilder TestContainerBuilder
);