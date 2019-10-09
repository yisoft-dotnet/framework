// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System.Collections.Generic;

namespace Yisoft.Framework.Collections.Generic
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1010:Collections should implement generic interface", Justification = "<挂起>")]
    public class GroupQueue<T> : Queue<T>
    {
        public GroupQueue() { }

        public GroupQueue(IEnumerable<T> collection) : base(collection) { }

        public GroupQueue(int capacity) : base(capacity) { }

        public List<T> Dequeue(int count)
        {
            var items = new List<T>();

            while (Count > 0)
            {
                items.Add(Dequeue());

                if (items.Count == count) break;
            }

            return items;
        }
    }
}