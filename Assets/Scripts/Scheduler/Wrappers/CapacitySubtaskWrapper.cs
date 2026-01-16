using System;
using Domain.Scheduler;
using UnityEngine;

namespace Scheduler.Wrappers
{
    public class CapacitySubtaskWrapper : SubtaskWrapper
    {
        public CapacitySubtaskWrapper(CapacitySubtask subtask) : base(subtask)
        {
        }

        public int SubtaskCapacity => CapacitySubtask.GetToolCapacity(ChosenToolId);
        public CapacitySubtask CapacitySubtask => (CapacitySubtask)Subtask;

        private int _currentToolFullness = 0;

        public override string SubtaskToolInfo => $"Вместительность: {SubtaskCapacity}";

        public override void PrecalculateSubtaskProgress(float workerEfficiencyCoeff, bool isLastTaskCell)
        {
            var currentToolCapacity = CapacitySubtask.GetToolCapacity(ChosenToolId);
            if (_currentToolFullness == currentToolCapacity || isLastTaskCell)
            {
                _currentToolFullness = 0;
                return;
            }

            var taskProgress = (int)(CapacitySubtask.GetEfficiency * workerEfficiencyCoeff);
            
            if (Subtask.TryGetWorkConstraint(out var maxCanBeDone))
            {
                if (taskProgress > maxCanBeDone) taskProgress = maxCanBeDone;
            }
            
            if (_currentToolFullness + taskProgress > currentToolCapacity)
                taskProgress = currentToolCapacity - _currentToolFullness;
            
            Subtask.CurrentDayProgress += taskProgress;
            _currentToolFullness += taskProgress;
            PrecalculatedProgress += taskProgress;
            Debug.Log($"{Subtask.Name}: Progress raised to {Subtask.CurrentDayProgress}");
        }

        public override int CalculateReferenceProgress(int scheduledOnHours)
        {
            var chosenToolCapacity = CapacitySubtask.GetToolCapacity(ChosenToolId);
            var currentToolFullness = 0;
            var referenceProgress = 0;

            for (var i = 0; i < scheduledOnHours; i++)
            {
                if (currentToolFullness == chosenToolCapacity || i == scheduledOnHours - 1) 
                {
                    currentToolFullness = 0;
                    continue;
                }
                
                var taskProgress = currentToolFullness + SubtaskEfficiency > chosenToolCapacity
                    ? chosenToolCapacity - currentToolFullness
                    : SubtaskEfficiency;
                referenceProgress += taskProgress;
                currentToolFullness += taskProgress;
            }
            return referenceProgress;
        }

        public override int SubtaskEfficiency => CapacitySubtask.GetEfficiency;
    }
}