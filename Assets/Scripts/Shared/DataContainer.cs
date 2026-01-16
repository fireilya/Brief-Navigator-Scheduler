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
        public static HashSet<Guid>  FoundRisksNeutralizerIds = new();
        public static Dictionary<Guid, GameTask[]> ChosenTasksByLocation = new();

        public static void Clear()
        {
            CurrentActionArea.Reset();
            ChosenTasks.Clear();
            FoundToolsIds.Clear();
            FoundRisksNeutralizerIds.Clear();
            ChosenTasksByLocation.Clear();
        }
    }
}
