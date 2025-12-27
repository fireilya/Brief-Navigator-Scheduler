using Domain.Scheduler;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Scheduler.Menu.Notebook
{
    public class ToolSlot : ItemSlot
    {
        [HideInInspector] public UnityEvent<ToolVariant> onToolAttached = new();

        public ToolVariant AttachedTool
        {
            get => (ToolVariant)AttachedItem;
            private set => AttachedItem = value;
        }

        public override void OnDrop(PointerEventData eventData)
        {
            var tool = eventData.pointerDrag.GetComponent<ToolVariant>();
            if (!tool) return;
            Attach(tool);
            onToolAttached.Invoke(tool);
        }
    }
}