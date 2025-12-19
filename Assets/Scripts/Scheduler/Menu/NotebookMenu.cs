using UnityEngine;

namespace Scheduler.Menu
{
    public class NotebookMenu : MenuBase
    {
        [Header("Data")]
        [SerializeField] private MapDataMapper mapper;
        [SerializeField] private NotebookCanvas notebookCanvas;
        protected override bool CanBeOpened => mapper.CurrentChosenLocation is not null;

        public override void Open()
        {
            base.Open();
            if (CanBeOpened) notebookCanvas.Init();
        }
    }
}