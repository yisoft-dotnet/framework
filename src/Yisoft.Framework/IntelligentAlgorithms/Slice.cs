// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Yisoft.Framework.IntelligentAlgorithms
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "<挂起>")]
    public struct Slice<T> : IEnumerable<T>, IEquatable<Slice<T>>
    {
        public Slice(T[] array) : this(array, 0, array?.Length ?? 0) { }

        public Slice(T[] array, int offset, int length)
        {
            if (offset + length > (array?.Length ?? 0)) throw new ArgumentException("Invalid slice");

            Array = array;
            Offset = offset;
            Length = length;
        }

        public T[] Array { get; }
        public int Offset { get; }
        public int Length { get; }

        public T this[int index]
        {
            get => Array[Offset + index];
            set => Array[Offset + index] = value;
        }

        public Slice<T> SubSlice(int relativeIndex, int length) { return new Slice<T>(Array, Offset + relativeIndex, length); }

        public Slice<T> SubSliceStart(int relativeIndex) { return new Slice<T>(Array, Offset, relativeIndex); }

        public Slice<T> SubSliceEnd(int relativeIndex) { return new Slice<T>(Array, Offset + relativeIndex, Length - relativeIndex); }

        public override string ToString() { return Array.Skip(Offset).Take(Length).ToString(); }

        public IEnumerator<T> GetEnumerator() { return Array.Skip(Offset).Take(Length).GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #region Equality members

        public bool Equals(Slice<T> other)
        {
            return Equals(Array, other.Array) && Offset == other.Offset && Length == other.Length;
        }

        public override bool Equals(object obj)
        {
            return obj is Slice<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Array != null ? Array.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Offset;
                hashCode = (hashCode * 397) ^ Length;

                return hashCode;
            }
        }

        public static bool operator ==(Slice<T> left, Slice<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Slice<T> left, Slice<T> right)
        {
            return !(left == right);
        }

        #endregion
    }
}