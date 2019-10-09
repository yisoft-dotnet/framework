// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Yisoft.Framework.IntelligentAlgorithms
{
    [SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "<挂起>")]
    public struct Choice<T> : IEquatable<Choice<T>>
    {
        public ChoiceType Type { get; }
        public T Removed { get; }
        public T Added { get; }

        public bool HasRemoved => Type != ChoiceType.Add;

        public bool HasAdded => Type != ChoiceType.Remove;

        internal Choice(ChoiceType type, T removed, T added)
        {
            Type = type;
            Removed = removed;
            Added = added;
        }

        public static Choice<T> Add(T value) { return new Choice<T>(ChoiceType.Add, default, value); }

        public static Choice<T> Remove(T value) { return new Choice<T>(ChoiceType.Remove, value, default); }

        public static Choice<T> Equal(T value) { return new Choice<T>(ChoiceType.Equal, value, value); }

        public static Choice<T> Substitute(T remove, T add) { return new Choice<T>(ChoiceType.Substitute, remove, add); }

        internal static Choice<T> Transpose(T remove, T add) { return new Choice<T>(ChoiceType.Transpose, remove, add); }

        public override string ToString()
        {
            switch (Type)
            {
                case ChoiceType.Equal:
                    return $"{Added}";
                case ChoiceType.Substitute:
                    return $"[-{Removed}+{Added}]";
                case ChoiceType.Remove:
                    return $"-{Removed}";
                case ChoiceType.Add:
                    return $"+{Added}";
                default:
                    return string.Empty;
            }
        }

        #region Equality members

        public bool Equals(Choice<T> other)
        {
            return Type == other.Type && EqualityComparer<T>.Default.Equals(Removed, other.Removed) && EqualityComparer<T>.Default.Equals(Added, other.Added);
        }

        public override bool Equals(object obj) { return obj is Choice<T> other && Equals(other); }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Type;
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(Removed);
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(Added);

                return hashCode;
            }
        }

        public static bool operator ==(Choice<T> left, Choice<T> right) { return left.Equals(right); }

        public static bool operator !=(Choice<T> left, Choice<T> right) { return !(left == right); }

        #endregion
    }
}