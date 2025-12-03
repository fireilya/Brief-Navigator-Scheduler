using System.Collections.Generic;

namespace DataFlowDemo.StaticData
{
    public static class DataContainer
    {
        public static List<Task> ChosenTasks = new List<Task>();
        public static List<Tool> FoundTools = new List<Tool>();
        public static List<Risk> Risks = new List<Risk>();

        public static void Clear()
        {
            ChosenTasks.Clear();
            FoundTools.Clear();
            Risks.Clear();
        }
    }
}
