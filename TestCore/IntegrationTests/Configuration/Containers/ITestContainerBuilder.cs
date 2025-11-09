namespace TestCore.IntegrationTests.Configuration.Containers;

public interface ITestContainerBuilder
{
    string ConnectionStringTemplate { get; }
    string Username { get; }
    string Password { get; }
    void WithPostgres();
    ContainerConfiguration Build();
}