using Domain.Scheduler;
using Scheduler.Bases;
using Scheduler.Wrappers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Scheduler.Menu.Calendar
{
    public class CalendarCell : SlotForDraggableBase, IPointerEnterHandler, IPointerExitHandler
    {
        public int Index { get; set; }
        public SubtaskWrapper ScheduledSubtask { get; set; }

        [HideInInspector] public UnityEvent<int, TaskPlane> onTaskPlaneEnter = new();

        [HideInInspector] public UnityEvent<int, TaskPlane> onTaskPlaneExit = new();

        [HideInInspector] public UnityEvent<int, TaskPlane> onTaskPlaneDropped = new();

        public bool IsScheduled => ScheduledSubtask != null;

        public override void OnDrop(PointerEventData eventData)
        {
            if (!eventData.dragging) return;
            var taskPlane = eventData.pointerDrag.GetComponent<TaskPlane>();
            if (taskPlane) onTaskPlaneDropped.Invoke(Index, taskPlane);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!eventData.dragging) return;
            var taskPlane = eventData.pointerDrag.GetComponent<TaskPlane>();
            if (taskPlane) onTaskPlaneEnter.Invoke(Index, taskPlane);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!eventData.dragging) return;
            var taskPlane = eventData.pointerDrag.GetComponent<TaskPlane>();
            if (taskPlane) onTaskPlaneExit.Invoke(Index, taskPlane);
        }
    }
}