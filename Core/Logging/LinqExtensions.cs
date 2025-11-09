using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logging;

public static class LinqExtensions
{
    public static void InsertBefore<TSource>(this IList<TSource> source, Func<TSource, bool> predicate, TSource element)
    {
        var beforeItem = source.FirstOrDefault(predicate);

        var insertIndex = beforeItem != null ? source.IndexOf(beforeItem) : 0;
        source.Insert(insertIndex, element);
    }
}