// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

namespace Yisoft.Framework
{
    public class ObjectCompareContext<T>
    {
        public ObjectCompareContext(T obj, int value = 0)
        {
            Obj = obj;
            Value = value;
        }

        public T Obj { get; }
        public int Value { get; internal set; }
    }
}