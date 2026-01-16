using System;
using Domain.Scheduler;
using UnityEngine;

namespace Scheduler.Wrappers
{
    public class ProcessSubtaskWrapper : SubtaskWrapper
    {
        public ProcessSubtaskWrapper(ProcessSubtask subtask) : base(subtask)
        {
        }

        public ProcessSubtask ProcessSubtask => (ProcessSubtask)Subtask;

        public override string SubtaskToolInfo => $"Коэффициент: {ProcessSubtask.GetWorkToolCoeff(ChosenToolId):F1}";

        public override void PrecalculateSubtaskProgress(float workerEfficiencyCoeff, bool isLastTaskCell)
        {
            var taskProgress = (int)(ProcessSubtask.GetWithToolEfficiency(ChosenToolId) * workerEfficiencyCoeff);
            if (Subtask.TryGetWorkConstraint(out var maxCanBeDone))
            {
                if (taskProgress > maxCanBeDone) taskProgress = maxCanBeDone;
            }
            ProcessSubtask.CurrentDayProgress += taskProgress;
            PrecalculatedProgress += taskProgress;
            Debug.Log($"{Subtask.Name}: Progress raised to {Subtask.CurrentDayProgress}");
        }

        public override int CalculateReferenceProgress(int scheduledOnHours)
        {
            return SubtaskEfficiency * scheduledOnHours;
        }

        public override int SubtaskEfficiency => ProcessSubtask.GetWithToolEfficiency(ChosenToolId);
    }
}