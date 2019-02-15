// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace Yisoft.Framework.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendJoin(
            this StringBuilder stringBuilder,
            IEnumerable<string> values,
            string separator = ", ")
        {
            return stringBuilder.AppendJoin(values, (sb, value) => sb.Append(value), separator);
        }

        public static StringBuilder AppendJoin<T>(
            this StringBuilder stringBuilder,
            IEnumerable<T> values,
            Action<StringBuilder, T> joinAction,
            string separator)
        {
            var appended = false;

            foreach (var value in values)
            {
                joinAction(stringBuilder, value);

                stringBuilder.Append(separator);

                appended = true;
            }

            if (appended) stringBuilder.Length -= separator.Length;

            return stringBuilder;
        }
    }
}