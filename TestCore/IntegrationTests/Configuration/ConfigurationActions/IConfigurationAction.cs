namespace TestCore.IntegrationTests.Configuration.ConfigurationActions;

public interface IConfigurationAction
{
    ConfigurationActionType Type { get; }
    ConfigurationActionType[] ActionTypesToExclude { get; }
    void Invoke(ConfigurationActionContext context);
}