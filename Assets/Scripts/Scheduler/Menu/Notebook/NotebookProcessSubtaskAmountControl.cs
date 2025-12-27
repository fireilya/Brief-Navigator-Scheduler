using Scheduler.Wrappers;

namespace Scheduler.Menu.Notebook
{
    public class NotebookProcessSubtaskAmountControl : NotebookSubtaskAmountControl
    {
        public ProcessSubtaskWrapper ProcessSubtaskWrapper => (ProcessSubtaskWrapper)SubtaskWrapper;
    }
}