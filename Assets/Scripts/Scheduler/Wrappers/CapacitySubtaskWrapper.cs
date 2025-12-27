using System;
using Domain.Scheduler;

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

        public override void Do(float efficiencyCoeff, bool isLastTaskCell)
        {
            var currentToolCapacity = CapacitySubtask.GetToolCapacity(ChosenToolId);
            if (_currentToolFullness == currentToolCapacity || isLastTaskCell)
            {
                _currentToolFullness = 0;
                return;
            }

            var taskProgress = (int)(CapacitySubtask.GetEfficiency * efficiencyCoeff);
            if (_currentToolFullness + taskProgress >= currentToolCapacity)
                taskProgress = currentToolCapacity - _currentToolFullness;
            CapacitySubtask.Progress += taskProgress;
            _currentToolFullness += taskProgress;
        }

        public override int SubtaskEfficiency => CapacitySubtask.GetEfficiency;
    }
}