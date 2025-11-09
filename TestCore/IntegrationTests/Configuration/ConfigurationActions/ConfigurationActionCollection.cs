using System.Collections.Generic;
using System.Linq;

namespace TestCore.IntegrationTests.Configuration.ConfigurationActions;

public class ConfigurationActionCollection : List<IConfigurationAction>
{
    public void AddActionWithRemovingExcludedActionTypes(IConfigurationAction action)
    {
        if (action.ActionTypesToExclude.Length > 0)
        {
            RemoveAll(x => action.ActionTypesToExclude.Contains(x.Type));
        }

        Add(action);
    }
}