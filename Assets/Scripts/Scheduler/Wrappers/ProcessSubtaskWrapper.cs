using System;
using Domain.Scheduler;

namespace Scheduler.Wrappers
{
    public class ProcessSubtaskWrapper : SubtaskWrapper
    {
        public ProcessSubtaskWrapper(ProcessSubtask subtask) : base(subtask)
        {
        }

        public ProcessSubtask ProcessSubtask => (ProcessSubtask)Subtask;

        public override string SubtaskToolInfo => $"Коэффициент: {ProcessSubtask.GetWorkToolCoeff(ChosenToolId):F1}";

        public override void Do(float efficiencyCoeff, bool isLastTaskCell)
        {
            ProcessSubtask.Progress += (int)(ProcessSubtask.GetToolEfficiency(ChosenToolId) * efficiencyCoeff);
        }

        public override int SubtaskEfficiency => ProcessSubtask.GetToolEfficiency(ChosenToolId);
    }
}