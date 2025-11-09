using System;
using System.Collections.Generic;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Testcontainers.PostgreSql;

namespace TestCore.IntegrationTests.Configuration.Containers;

public class TestContainerBuilder : ITestContainerBuilder
{
    private const int PostgresContainerPort = 5432;
    private const string DataBaseName = "testdb";
    private const string NetworkAliases = "postgres";
    private readonly List<IContainer> containers = [];

    private readonly INetwork network = new NetworkBuilder().Build();
    private readonly int postgresHostPort = Random.Shared.Next(9_000, 10_000);

    public string ConnectionStringTemplate =>
        $"Host=127.0.0.1;Port={postgresHostPort};Database={DataBaseName};Username={{0}};Password={{1}}";

    public string Username { get; } = Guid.NewGuid().ToString();
    public string Password { get; } = Guid.NewGuid().ToString();

    public void WithPostgres()
    {
        containers.Add(
            new PostgreSqlBuilder()
               .WithImage("postgres:16")
               .WithPortBinding(postgresHostPort, PostgresContainerPort)
               .WithDatabase(DataBaseName)
               .WithUsername(Username)
               .WithPassword(Password)
               .WithNetwork(network)
               .WithNetworkAliases(NetworkAliases)
               .Build()
        );
    }

    public ContainerConfiguration Build()
    {
        return new ContainerConfiguration(
            network,
            containers.ToArray()
        );
    }
}