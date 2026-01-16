using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scheduler.Bases
{
    public class SlotForDraggableBase : MonoBehaviour, IDropHandler
    {
        protected DraggableMenuItemBase AttachedItem;
        public const float PlacedObjectRelativeScaleRatio = 0.95f;
        public RectTransform RectTransform { get; private set; }
        
        public Image Image { get; private set; }
        public bool HasAttach => AttachedItem;

        [HideInInspector]
        public UnityEvent<DraggableMenuItemBase> onAttached = new();
        
        [HideInInspector]
        public UnityEvent<DraggableMenuItemBase> onDetached = new();

        protected virtual void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            Image = GetComponent<Image>();
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            var draggable = eventData.pointerDrag.GetComponent<DraggableMenuItemBase>();
            if (!draggable) return;
            Attach(draggable);
        }

        protected virtual void Attach(DraggableMenuItemBase item)
        {
            item.AttachedTo = this;
            item.transform.SetParent(transform);
            item.RectTransform.position = RectTransform.position;
            item.RectTransform.sizeDelta = RectTransform.sizeDelta * PlacedObjectRelativeScaleRatio;
            AttachedItem = item;
            onAttached.Invoke(item);
        }

        public virtual DraggableMenuItemBase Detach()
        {
            if (!AttachedItem) return null;
            var item = AttachedItem;
            AttachedItem = null;
            item.AttachedTo = null;
            onDetached.Invoke(item);
            return item;
        }
    }
}