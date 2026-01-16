using System;
using Scheduler.Bases;
using Scheduler.Wrappers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Scheduler.Menu.Calendar
{
    public class CalendarCell : SlotForDraggableBase, IPointerEnterHandler, IPointerExitHandler
    {
        public int Index
        {
            get => _index;
            set
            {
                if (_index != -1) throw new Exception("CalendarCell index reassign not allowed!");
                _index = value;
            }
        }

        public ScheduledCellsMark ScheduledByMark
        {
            get => _scheduledByMark;
            set
            {
                if (_scheduledByMark) throw new Exception("ScheduledByMark reassign not allowed!");
                _scheduledByMark = value;
            }
        }

        public SubtaskWrapper ScheduledSubtaskWrapper => ScheduledByMark.SubtaskWrapper;

        [HideInInspector] public UnityEvent<int, TaskPlane> onTaskPlaneEnter = new();

        [HideInInspector] public UnityEvent<int, TaskPlane> onTaskPlaneExit = new();

        [HideInInspector] public UnityEvent<int, TaskPlane> onTaskPlaneDropped = new();
        private int _index = -1;
        private ScheduledCellsMark _scheduledByMark;

        public bool IsScheduled => ScheduledByMark;

        public void ResetScheduling() => _scheduledByMark = null;

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