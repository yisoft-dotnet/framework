// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Globalization;

namespace Yisoft.Framework.Extensions
{
    /// <summary>
    /// make write compareTo simpler.
    /// </summary>
    /// <example>
    /// public override int CompareTo(DataDictionary other)
    /// {
    ///     return this.CompareWith(other)
    ///     .Then(x => Nullable.Compare(x.ParentId, other.ParentId))
    ///     .Then(x => Nullable.Compare(other.Rank, x.Rank))
    ///     .Then(x => x.DisplayName.CompareTo(other.DisplayName, CompareOptions.IgnoreCase))
    ///     .Then(x => x.Value.CompareTo(other.Value, CompareOptions.IgnoreCase))
    ///     .Then(x => x.Key.CompareTo(other.Key, CompareOptions.IgnoreCase))
    ///     .Then(x => x.Id.CompareTo(other.Id))
    ///     .Value;
    /// }
    /// </example>
    public static class ObjectCompareExtensions
    {
        private static readonly CultureInfo _ZhCNCulture = CultureInfo.GetCultureInfo("zh-cn");

        public static ObjectCompareContext<T> CompareWith<T>(this T obj, T other)
        {
            return new ObjectCompareContext<T>(obj)
            {
                Value = obj == null || other == null
                    ? obj == null ? -1 : 1
                    : 0
            };
        }

        public static ObjectCompareContext<T> Then<T>(this ObjectCompareContext<T> context, Func<T, int> compareToFunc)
        {
            if (context.Value != 0) return context;

            context.Value = compareToFunc(context.Obj);

            return context;
        }

        public static int CompareTo(this string a, string b, CompareOptions options) { return CompareTo(a, b, _ZhCNCulture, options); }

        public static int CompareTo(this string a, string b, CultureInfo culture, CompareOptions options = CompareOptions.None)
        {
            return string.Compare(a, b, culture, options);
        }

        public static int End<T>(this ObjectCompareContext<T> context) { return context.Value; }
    }
}