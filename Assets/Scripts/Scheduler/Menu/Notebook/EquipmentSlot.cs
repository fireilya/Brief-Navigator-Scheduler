using Domain.Scheduler;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Scheduler.Menu.Notebook
{
    public class EquipmentSlot : ItemSlot
    {
        [HideInInspector]
        public UnityEvent<EquipmentVariant> OnEquipmentAttached = new();
        public EquipmentVariant AttachedEquipment
        {
            get => (EquipmentVariant)AttachedItem;
            private set => AttachedItem = value;
        }

        public override void OnDrop(PointerEventData eventData)
        {
            var equipment = eventData.pointerDrag.GetComponent<EquipmentVariant>();
            if (!equipment) return;
            Attach(equipment);
            OnEquipmentAttached.Invoke(equipment);
        }
    }
}