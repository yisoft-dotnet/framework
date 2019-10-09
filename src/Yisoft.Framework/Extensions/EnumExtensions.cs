// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Linq;
using System.Reflection;
using Yisoft.Framework.Collections.Generic;

namespace Yisoft.Framework.Extensions
{
    /// <summary>
    /// 为 <see cref="Enum"/> 对象提供扩展方法。
    /// </summary>
    public static class EnumExtensions
    {
        private static readonly ThreadSafeSortedList<string, object> _Caches = new ThreadSafeSortedList<string, object>();

        /// <summary>
        /// 返回当前枚举值描述特性的实例。
        /// </summary>
        /// <param name="enumObj">枚举值。</param>
        /// <returns>返回 <see cref="EnumExtraAttribute"/> 对象的实例。</returns>
        public static EnumExtraAttribute GetDescription(this Enum enumObj) { return GetDescription<EnumExtraAttribute>(enumObj); }

        /// <summary>
        /// 返回当前枚举值描述特性的实例。
        /// </summary>
        /// <typeparam name="T">要获取的对象的类型。</typeparam>
        /// <param name="enumObj">枚举值。</param>
        /// <returns>返回 <typeparamref name="T"/> 对象的实例。</returns>
        public static T GetDescription<T>(this Enum enumObj) where T : EnumExtraAttribute
        {
            if (enumObj == null) throw new ArgumentNullException(nameof(enumObj));

            var enumType = enumObj.GetType();
            var attrType = typeof(T);
            var cacheKey = $"{enumType.FullName}`{enumObj}`{attrType.FullName}";

            if (_Caches[cacheKey] is T descriptionObject) return descriptionObject;

            var fieldName = enumObj.ToString();
            var fieldInfo = enumType.GetTypeInfo().GetField(fieldName);

            descriptionObject = fieldInfo.GetCustomAttributes(attrType, true).FirstOrDefault() as T;

            if (descriptionObject == null) return null;

            _Caches[cacheKey] = descriptionObject;

            return descriptionObject;
        }

        /// <summary>
        /// 返回当前枚举值描述特性的值。
        /// </summary>
        /// <typeparam name="T">要获取的对象的类型。</typeparam>
        /// <typeparam name="TValue">要获取的对象的值的类型。</typeparam>
        /// <param name="enumObj">枚举值。</param>
        /// <param name="getValueFunction">指定如何获取对象的值。</param>
        /// <param name="defaultValue">在方法返回时，如果没有获取到有效的值，则使用该默认值代替。</param>
        /// <returns>返回 <typeparamref name="TValue"/> 对象。</returns>
        public static TValue GetDescriptionValue<T, TValue>(this Enum enumObj,
            Func<T, TValue> getValueFunction,
            TValue defaultValue = default)
            where T : EnumExtraAttribute
        {
            if (getValueFunction == null) throw new ArgumentNullException(nameof(getValueFunction));

            var description = GetDescription<T>(enumObj);

            if (description == null) return defaultValue;

            var value = getValueFunction(description);

            return Equals(value, default(TValue)) ? defaultValue : value;
        }

        /// <summary>
        /// 返回当前枚举值的数字值。
        /// </summary>
        /// <param name="enumObj">枚举值。</param>
        /// <returns>返回 <see cref="int"/>。</returns>
        public static int ToInt32(this Enum enumObj) { return Convert.ToInt32(enumObj); }

        /// <summary>
        /// 返回当前枚举值的数字值。
        /// </summary>
        /// <param name="enumObj">枚举值。</param>
        /// <returns>返回 <see cref="long"/>。</returns>
        public static long ToInt64(this Enum enumObj) { return Convert.ToInt64(enumObj); }

        public static EnumExtraInfo GetExtra(this Enum enumObj)
        {
            var d = GetDescription(enumObj);
            var name = enumObj?.ToString();
            var value = enumObj.ToInt64();

            return d == null ? new EnumExtraInfo(name, name, value) : d.GetExtra(enumObj);
        }
    }
}