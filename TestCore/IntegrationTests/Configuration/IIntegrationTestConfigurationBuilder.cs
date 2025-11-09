using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestCore.IntegrationTests.Configuration;

public interface IIntegrationTestConfigurationBuilder
{
    IIntegrationTestConfigurationBuilder WithNullLogger();
    IIntegrationTestConfigurationBuilder WithRealLogger();
    IIntegrationTestConfigurationBuilder CustomizeServiceCollection(Action<IServiceCollection> customizer);
    IIntegrationTestConfigurationBuilder CustomizeConfigurationManager(Action<IConfigurationManager> customizer);
    IIntegrationTestConfigurationBuilder WithDataBase();
    IntegrationTestConfiguration Build();
}