using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestCore.IntegrationTests.Configuration.ConfigurationActions;
using TestCore.IntegrationTests.Configuration.Containers;

namespace TestCore.IntegrationTests.Configuration;

public static class IntegrationTestConfigurationBuilderFactory
{
    public static IIntegrationTestConfigurationBuilder Create(Assembly targetAssembly) =>
        new IntegrationTestConfigurationBuilder(
            new ConfigurationActionContext(
                new ServiceCollection(),
                new ConfigurationManager(),
                new TestContainerBuilder()
            )
        );
}