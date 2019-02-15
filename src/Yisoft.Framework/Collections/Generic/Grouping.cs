// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Yisoft.Framework.Collections.Generic
{
    [DebuggerDisplay("Key = {Key}  Count = {Count}")]
    [DebuggerTypeProxy(typeof(Proxy))]
    public class Grouping<TKey, T> : List<T>, IGrouping<TKey, T>
    {
        public Grouping(TKey key) { Key = key; }

        public Grouping(TKey key, IEnumerable<T> values)
        {
            Key = key;

            AddRange(values);
        }

        public TKey Key { get; }
    }
}