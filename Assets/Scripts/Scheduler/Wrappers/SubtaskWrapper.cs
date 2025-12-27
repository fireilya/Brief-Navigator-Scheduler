using System;
using Domain.Scheduler;
using UnityEngine.Events;

namespace Scheduler.Wrappers
{
    public abstract class SubtaskWrapper
    {
        protected SubtaskWrapper(Subtask subtask)
        {
            Subtask = subtask;
        }

        public bool IsCapacityTask => Subtask.IsUseCapacityTool;
        public string SubtaskName => Subtask.Name;
        public abstract string SubtaskToolInfo { get; }

        public Subtask Subtask { get; set; }
        public UnityEvent<Guid> OnToolChanges = new();

        public Guid ChosenToolId
        {
            get => _chosenToolId;
            set
            {
                if (value == _chosenToolId) return;
                _chosenToolId = value;
                OnToolChanges.Invoke(value);
            }
        }
        private Guid _chosenToolId = Guid.Empty;

        public Neutralizer RiskNeutralizer { get; set; } = null;
        public abstract void Do(float efficiencyCoeff, bool isLastTaskCell);

        public abstract int SubtaskEfficiency { get; }

        public void ResetChosenTool() => ChosenToolId = Guid.Empty;
    }
}