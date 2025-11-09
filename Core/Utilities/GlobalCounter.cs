using System.Collections.Concurrent;
using System.Threading;

namespace Core.Utilities;

public static class GlobalCounter
{
    private static readonly ConcurrentDictionary<string, int> counters = new();
    private static readonly Lock lockObject = new();

    public static int GetCount(string countKey)
    {
        lock (lockObject)
        {
            if (!counters.TryGetValue(countKey, out var count))
            {
                counters[countKey] = count = 1;
            }

            return count;
        }
    }

    public static int GetCountWithIncrement(string countKey)
    {
        lock (lockObject)
        {
            if (!counters.TryGetValue(countKey, out var count))
            {
                counters[countKey] = count = 1;
            }

            counters[countKey]++;
            return count;
        }
    }
}