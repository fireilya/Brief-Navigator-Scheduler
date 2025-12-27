using Domain.Scheduler;
using Scheduler.Bases;
using UnityEngine;

namespace Scheduler.Menu.Notebook
{
    public class ToolsMenu : MenuForDragableItemsBase
    {
        [SerializeField] private ToolVariant toolVariantPrefab;
        public void CreateItem(Tool tool)
        {
            var newToolVariant = Instantiate(toolVariantPrefab);
            AddItem(newToolVariant);
            newToolVariant.Tool = tool;
        }
    }
}
