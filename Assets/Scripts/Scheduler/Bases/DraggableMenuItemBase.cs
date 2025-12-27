using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Scheduler.Bases
{
    public class DraggableMenuItemBase : DragableBase
    {
        protected MenuForDragableItemsBase Menu;
        public SlotForDraggableBase AttachedTo { get; set; }
        public bool IsCatched => AttachedTo;
        
        [HideInInspector]
        public UnityEvent onCatched = new();

        protected override void Start()
        {
            base.Start();
            Menu = GetComponentInParent<MenuForDragableItemsBase>();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            if (AttachedTo) AttachedTo.Detach();
            transform.SetParent(Canvas.transform);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (!IsCatched) Menu.ReturnItem(this);
        }
    }
}