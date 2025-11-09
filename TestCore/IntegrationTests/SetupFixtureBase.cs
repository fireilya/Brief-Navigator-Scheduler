using System.Threading.Tasks;
using Core.EFCore;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TestCore.IntegrationTests.Configuration;

namespace TestCore.IntegrationTests;

[SetUpFixture]
public abstract class SetupFixtureBase
{
    public static IntegrationTestConfiguration TestConfiguration { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var integrationTestConfigurationBuilder = IntegrationTestConfigurationBuilderFactory
           .Create()
           .CustomizeConfigurationManager(CustomizeConfiguration)
           .CustomizeServiceCollection(CustomizeServiceCollection)
           .WithRealLogger()
           .WithDataBase();
        CustomizeConfigurationBuilder(integrationTestConfigurationBuilder);

        TestConfiguration = integrationTestConfigurationBuilder.Build();
        await TestConfiguration.ContainerConfiguration.StartAsync(OnContainerStart);

        await InnerOneTimeSetup();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await TestConfiguration.ContainerConfiguration.DisposeAsync();
    }

    protected virtual void CustomizeConfiguration(IConfigurationManager configurationManager) { }
    protected virtual void CustomizeServiceCollection(IServiceCollection serviceCollection) { }
    protected virtual void CustomizeConfigurationBuilder(IIntegrationTestConfigurationBuilder builder) { }

    protected virtual async Task OnContainerStart(IContainer container)
    {
        var dataContext = TestConfiguration.ServiceProvider.GetRequiredService<DataContext>();
        await dataContext.Database.EnsureCreatedAsync();
    }

    protected virtual Task InnerOneTimeSetup() => Task.CompletedTask;
}