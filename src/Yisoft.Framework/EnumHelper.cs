// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework
{
    /// <summary>
    /// 包含一组方法和属性，提供枚举值的实用功能。
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 表示对 <see cref="Constellation"/> 枚举值的实用操作的封装。
        /// </summary>
        public static readonly EnumVictor<Constellation, ConstellationDescriptionAttribute> StarSignsVictor =
            new EnumVictor<Constellation, ConstellationDescriptionAttribute>();

        /// <summary>
        /// 获取字符串代表的枚举值。
        /// </summary>
        /// <typeparam name="T">指定表示枚举值的 <see cref="System.Type"/>。</typeparam>
        /// <param name="str">表示枚举项的字符串。</param>
        /// <returns>返回类型为 <typeparamref name="T"/> 的枚举值。</returns>
        public static T GetEnum<T>(string str) { return (T) Enum.Parse(typeof(T), str, true); }

        public static HashSet<T> GetEnumFields<T>()
        {
            var enumType = typeof(T);

            if (enumType == null) throw new TypeAccessException();
            if (enumType.GetTypeInfo().IsEnum == false) throw new TypeAccessException();

            var enumFields = Enum.GetValues(enumType);
            var result = new HashSet<T>();

            foreach (var enumField in enumFields) result.Add((T) enumField);

            return result;
        }

        public static List<EnumExtraInfo> GetExtras<T>() { return GetExtras(typeof(T)); }

        public static List<EnumExtraInfo> GetExtras(Type enumType)
        {
            if (enumType == null) throw new ArgumentNullException(nameof(enumType));
            if (enumType.GetTypeInfo().IsEnum == false) throw new ArgumentException("The type is not a enum.", nameof(enumType));

            var enumFields = Enum.GetValues(enumType);

            return (from object enumField in enumFields
                    let extra = ((Enum) enumField).GetExtra()
                    where extra != null
                    select ((Enum) enumField).GetExtra()).ToList();
        }

        /// <summary>
        /// 返回指定枚举项的位域子项的集合，无论是否指定了 <see cref="System.FlagsAttribute"/> 属性，都将尽量返回最小枚举项的集合。
        /// </summary>
        /// <typeparam name="T">指定表示枚举值的 <see cref="System.Type"/>。</typeparam>
        /// <param name="enumObj">指定一个枚举项。</param>
        /// <returns>返回指定枚举项的位域子项的集合。</returns>
        public static HashSet<T> GetBitFields<T>(Enum enumObj)
        {
            var enumType = typeof(T);

            if (enumType == null) throw new TypeAccessException(nameof(enumType));
            if (enumType.GetTypeInfo().IsEnum == false) throw new ArgumentException(nameof(enumType));
            if (enumObj == null) throw new ArgumentNullException(nameof(enumObj));

            var x = Convert.ToInt32(enumObj);
            var result = new HashSet<T>();

            // 指定枚举值不可再分时直接返回
            if (x == 0 || (x & (x - 1)) == 0)
            {
                result.Add((T) (enumObj as object));

                return result;
            }

            var enumValues = Enum.GetValues(enumType);

            result.IntersectWith(from object enumField in enumValues
                                 let y = Convert.ToInt32(enumField)
                                 where y != 0
                                 where (y & (y - 1)) == 0
                                 where (x & y) == y
                                 select (T) enumField);

            return result;
        }

        /// <summary>
        /// 返回表示指定枚举项的位域子项的集合的字符串，无论是否指定了 <see cref="System.FlagsAttribute"/> 属性，都将尽量返回最小枚举项的集合。
        /// </summary>
        /// <typeparam name="T">指定表示枚举值的 <see cref="System.Type"/>。</typeparam>
        /// <param name="enumObj">指定一个枚举项。</param>
        /// <returns>返回表示指定枚举项的位域子项的集合的字符串。</returns>
        public static string GetBitFieldsString<T>(Enum enumObj)
        {
            var list = GetBitFields<T>(enumObj);

            if (list == null || list.Count == 0) return string.Empty;

            var result = new StringBuilder();

            foreach (var item in list)
            {
                if (result.Length == 0) result.Append(item);
                else result.Append(", ").Append(item);
            }

            return result.ToString();
        }
    }
}