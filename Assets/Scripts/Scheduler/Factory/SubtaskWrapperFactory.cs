using Domain.Scheduler;
using Scheduler.Wrappers;

namespace Scheduler.Factory
{
    public static class SubtaskWrapperFactory
    {
        public static SubtaskWrapper CreateSubtaskWrapper(Subtask subtask)
        {
            if (subtask.IsUseCapacityTool) return new CapacitySubtaskWrapper((CapacitySubtask)subtask);
            return new ProcessSubtaskWrapper((ProcessSubtask)subtask);
        }
    }
}