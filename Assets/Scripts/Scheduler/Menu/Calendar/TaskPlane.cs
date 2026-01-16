using Domain.Scheduler;
using Scheduler.Bases;
using Scheduler.Wrappers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scheduler.Menu.Calendar
{
    public class TaskPlane : DraggableMenuItemBase, IPointerClickHandler
    {
        private SubtaskWrapper _subtaskWrapper;
        private int _scheduledOnHours = 4;
        private TMP_Text _taskInfo;
        public string TaskInfoText => _taskInfo.text;
        public CalendarGrid CalendarGrid { get; set; }

        public SubtaskWrapper SubtaskWrapper
        {
            get => _subtaskWrapper;
            set
            {
                _subtaskWrapper = value;
                RecalculateTaskInfoLabel();
            }
        }

        public int ScheduledOnHours
        {
            get => _scheduledOnHours;
            set
            {
                _scheduledOnHours = value;
                RecalculateTaskInfoLabel();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _taskInfo = GetComponentInChildren<TMP_Text>();
            RecalculateTaskInfoLabel();
        }

        public void Return()
        {
            CanvasGroup.blocksRaycasts = true;
            Menu.ReturnItem(this);
        }

        private void RecalculateTaskInfoLabel()
        {
            var taskLabel = SubtaskWrapper == null ? "" : SubtaskWrapper.SubtaskName;
            var taskAmountLabel = SubtaskWrapper?.CalculateReferenceProgress(ScheduledOnHours) ?? 0;
            _taskInfo?.SetText($"{taskLabel};\n{taskAmountLabel} штук, {ScheduledOnHours} часа");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                CalendarGrid.ShowWarningWindow(
                    $"Вы уверены, что хотите удалить задачу \"{TaskInfoText.Replace('\n', ' ')}\"?",
                    continueCallback: () => Destroy(gameObject));
            }
        }
    }
}