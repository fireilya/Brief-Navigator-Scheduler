using System;
using System.Collections.Generic;
using Domain.Scheduler;

namespace Scheduler.Data
{
    public static class DataContainer
    {
        public static Guid QuestId = Guid.NewGuid();
        public static ActionArea CurrentActionArea;
        public static List<GameTask> ChosenTasks = new();
        public static HashSet<Guid> FoundToolsIds = new();
        public static List<Neutralizer> FoundRisksNeutralizes = new();
        public static Dictionary<Guid, GameTask[]> ChosenTasksByLocation = new();

        public static void Clear()
        {
            ChosenTasks.Clear();
            FoundToolsIds.Clear();
            FoundRisksNeutralizes.Clear();
            ChosenTasksByLocation.Clear();
        }
    }
}
