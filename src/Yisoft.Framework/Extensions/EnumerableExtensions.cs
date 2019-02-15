// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Yisoft.Framework.Extensions
{
    [DebuggerStepThrough]
    public static class EnumerableExtensions
    {
        public static string Join(this IEnumerable<object> source, string separator = ", ") { return string.Join(separator, source); }

        public static IEnumerable<T> Distinct<T>(
            this IEnumerable<T> source, Func<T, T, bool> comparer)
            where T : class
        {
            return source.Distinct(new DynamicEqualityComparer<T>(comparer));
        }

        public static string ToString<T>(this IEnumerable<T> source, string separator)
        {
            StringBuilder sb = null;

            foreach (var item in source)
            {
                if (sb == null) sb = new StringBuilder();
                else sb.Append(separator);

                sb.Append(item);
            }

            return sb == null ? string.Empty : sb.ToString();
        }

        public static string ToString<T>(this IEnumerable<T> source, Func<T, string> toString, string separator)
        {
            StringBuilder sb = null;

            foreach (var item in source)
            {
                if (sb == null) sb = new StringBuilder();
                else sb.Append(separator);

                sb.Append(toString(item));
            }

            return sb == null ? string.Empty : sb.ToString();
        }

        private sealed class DynamicEqualityComparer<T> : IEqualityComparer<T>
            where T : class
        {
            private readonly Func<T, T, bool> _func;

            public DynamicEqualityComparer(Func<T, T, bool> func) { _func = func; }

            public bool Equals(T x, T y) { return _func(x, y); }

            public int GetHashCode(T obj)
            {
                return 0; // force Equals
            }
        }
    }
}