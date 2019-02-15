// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Yisoft.Framework.Collections.Generic;

namespace Yisoft.Framework.Extensions
{
    public static class GroupExtensions
    {
        public static IEnumerable<IGrouping<TKey, T>> GroupWhenChange<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> getGroupKey)
        {
            Grouping<TKey, T> current = null;

            foreach (var item in collection)
            {
                if (current == null)
                {
                    current = new Grouping<TKey, T>(getGroupKey(item))
                    {
                        item
                    };
                }
                else if (current.Key.Equals(getGroupKey(item))) current.Add(item);
                else
                {
                    yield return current;

                    current = new Grouping<TKey, T>(getGroupKey(item))
                    {
                        item
                    };
                }
            }

            if (current != null) yield return current;
        }
    }
}