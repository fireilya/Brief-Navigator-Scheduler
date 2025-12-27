using Domain.Scheduler;
using Shared;
using UnityEngine;

namespace Scheduler.Menu.Notebook
{
    public class ToolVariant : ItemVariant
    {
        private Tool _tool;

        public Tool Tool
        {
            get => _tool;
            set
            {
                _tool = value;
                Icon.sprite = ImageServerMock.LoadImage(_tool?.PathToIcon);
            }
        }
    }
}
