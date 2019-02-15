// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

namespace Yisoft.Framework.IntelligentAlgorithms
{
    public struct DiffPair<T>
    {
        public DiffPair(DiffAction action, T value)
        {
            Action = action;
            Value = value;
        }

        public readonly DiffAction Action;
        public readonly T Value;

        public override string ToString()
        {
            var str = Action == DiffAction.Added ? "+" : Action == DiffAction.Removed ? "-" : string.Empty;

            return str + Value;
        }
    }
}