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
    public struct Slice<T> : IEnumerable<T>
    {
        public Slice(T[] array) : this(array, 0, array.Length) { }

        public Slice(T[] array, int offset, int length)
        {
            if (offset + length > array.Length) throw new ArgumentException("Invalid slice");

            Array = array;
            Offset = offset;
            Length = length;
        }

        public readonly T[] Array;
        public readonly int Offset;
        public readonly int Length;

        public T this[int index]
        {
            get
            {
                if (index > Length) throw new IndexOutOfRangeException();

                return Array[Offset + index];
            }
            set
            {
                if (index > Length) throw new IndexOutOfRangeException();

                Array[Offset + index] = value;
            }
        }

        public Slice<T> SubSlice(int relativeIndex, int length) { return new Slice<T>(Array, Offset + relativeIndex, length); }

        public Slice<T> SubSliceStart(int relativeIndex) { return new Slice<T>(Array, Offset, relativeIndex); }

        public Slice<T> SubSliceEnd(int relativeIndex) { return new Slice<T>(Array, Offset + relativeIndex, Length - relativeIndex); }

        public override string ToString() { return Array.Skip(Offset).Take(Length).ToString(); }

        public IEnumerator<T> GetEnumerator() { return Array.Skip(Offset).Take(Length).GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}