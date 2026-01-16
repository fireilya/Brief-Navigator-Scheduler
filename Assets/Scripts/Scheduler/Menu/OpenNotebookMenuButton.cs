using UnityEngine;

namespace Scheduler.Menu
{
    public class OpenNotebookMenuButton : OpenMenuButton
    {
        [SerializeField] private MapDataMapper map;
        protected override void Open()
        {
            if (!map.IsLocationSelected) return;
            base.Open();
        }
    }
}