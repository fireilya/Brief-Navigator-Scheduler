using Domain.Scheduler;
using Shared;
using UnityEngine;

namespace Scheduler.Menu.Journal
{
    public class ToolJournalIcon : JournalIcon
    {
        private Tool _tool;

        public Tool Tool    
        {
            get => _tool;
            set
            {
                _tool = value;
                Icon.sprite = _tool != null ? ImageServerMock.LoadImage(_tool.PathToIcon) : null;
            }
        }

        protected override void ShowHint(Vector2 pointerPosition)
        {
            base.ShowHint(pointerPosition);
            HintPlane.Show(Tool.Name, pointerPosition);
        }
    }
}