using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LazyAbp.Abp.AuthCenter.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> TakeIf<T, TKey>(this IEnumerable<T> query, Func<T, TKey> orderBy, bool condition, int limit, bool orderByDescending = true)
        {
            // It is necessary sort items before it
            query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            return condition
                ? query.Take(limit)
                : query;
        }
    }
}
