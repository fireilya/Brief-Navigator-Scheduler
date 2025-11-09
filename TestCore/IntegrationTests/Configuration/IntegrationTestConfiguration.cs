using System;
using TestCore.IntegrationTests.Configuration.Containers;

namespace TestCore.IntegrationTests.Configuration;

public record IntegrationTestConfiguration(
    IServiceProvider ServiceProvider,
    ContainerConfiguration ContainerConfiguration
);