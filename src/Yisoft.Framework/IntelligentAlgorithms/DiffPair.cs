// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;

namespace Yisoft.Framework.IntelligentAlgorithms
{
    public struct DiffPair<T> : IEquatable<DiffPair<T>>
    {
        public DiffPair(DiffAction action, T value)
        {
            Action = action;
            Value = value;
        }

        public DiffAction Action { get; }
        public T Value { get; }

        public override string ToString()
        {
            var str = Action == DiffAction.Added ? "+" : Action == DiffAction.Removed ? "-" : string.Empty;

            return str + Value;
        }

        #region Equality members

        public bool Equals(DiffPair<T> other) { return Action == other.Action && EqualityComparer<T>.Default.Equals(Value, other.Value); }

        public override bool Equals(object obj) { return obj is DiffPair<T> other && Equals(other); }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Action * 397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
            }
        }

        public static bool operator ==(DiffPair<T> left, DiffPair<T> right) { return left.Equals(right); }

        public static bool operator !=(DiffPair<T> left, DiffPair<T> right) { return !(left == right); }

        #endregion
    }
}