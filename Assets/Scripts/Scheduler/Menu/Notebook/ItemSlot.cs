using Scheduler.Bases;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scheduler.Menu.Notebook
{
    public class ItemSlot : SlotForDraggableBase
    {
        protected TMP_Text SlotLabel;

        protected override void Awake()
        {
            base.Awake();
            SlotLabel = GetComponentInChildren<TMP_Text>();
        }

        public void SetSlotLabel(string label)
        {
            SlotLabel?.SetText(label);
        }
        
        public void ClearSlotLabel() => SlotLabel?.SetText("");
    }
}