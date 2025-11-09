using System;
using System.Threading.Tasks;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;

namespace TestCore.IntegrationTests.Configuration.Containers;

public record ContainerConfiguration(
    INetwork Network,
    IContainer[] Containers
)
{
    public async Task StartAsync(Func<IContainer, Task> onStart)
    {
        await Network.CreateAsync();
        foreach (var container in Containers)
        {
            await container.StartAsync();
            await onStart(container);
        }
    }

    public async Task DisposeAsync()
    {
        await Network.DisposeAsync();
        foreach (var container in Containers)
        {
            await container.DisposeAsync();
        }
    }
}