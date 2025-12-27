using System;
using Domain.Scheduler;
using Scheduler.Wrappers;
using TMPro;
using UnityEngine;

namespace Scheduler.Menu.Notebook
{
    public class NotebookCapacitySubtaskAmountControl : NotebookSubtaskAmountControl
    {
        [SerializeField] private TMP_Text instrumentFullnessText;
        [SerializeField] private TMP_Text unloadHoursAmountText;

        public CapacitySubtaskWrapper CapacitySubtaskWrapper => (CapacitySubtaskWrapper)SubtaskWrapper;

        public int UnloadHours { get; private set; }
        
        public override int TotalHours => WorkHours + UnloadHours;

        protected override void ConfigureButtons()
        {
            moreButton.onClick.AddListener(() => OnAmountButtonClicked(true));
            lessButton.onClick.AddListener(() => OnAmountButtonClicked(false));
            lessButton.interactable = false;
        }

        protected override void OnAmountButtonClicked(bool isIncreaseButton)
        {
            WorkHours += isIncreaseButton ? 1 : -1;
            onWorkHoursChanged.Invoke(WorkHours);
            lessButton.interactable = WorkHours != 0;
            UnloadHours = (int)Math.Ceiling(
                (decimal)(SubtaskWrapper.SubtaskEfficiency * WorkHours) / CapacitySubtaskWrapper.SubtaskCapacity);
            UpdateWorkInfoLabels();
        }

        public override void UpdateWorkInfoLabels()
        {
            base.UpdateWorkInfoLabels();
            
            UnloadHours = (int)Math.Ceiling(
                (decimal)(SubtaskWrapper.SubtaskEfficiency * WorkHours) / CapacitySubtaskWrapper.SubtaskCapacity);
            
            unloadHoursAmountText.SetText($"+{UnloadHours}");

            var instrumentFullness = WorkHours == 0
                ? 0
                : SubtaskWrapper.SubtaskEfficiency * WorkHours -
                  CapacitySubtaskWrapper.SubtaskCapacity * (UnloadHours - 1);

            instrumentFullnessText.SetText(
                $"{instrumentFullness}/{CapacitySubtaskWrapper.SubtaskCapacity}");
        }

        public override void Clear()
        {
            base.Clear();
            UnloadHours = 0;
        }
    }
}