using Scheduler.Bases;
using Scheduler.Menu.Notebook;
using Shared;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scheduler
{
    public class ItemVariant : DraggableMenuItemBase
    {
        public Image Icon { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Icon = GetComponent<Image>();
        }
    }
}
