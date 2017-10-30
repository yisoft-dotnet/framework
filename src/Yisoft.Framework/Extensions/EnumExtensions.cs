//      )                             *     
//   ( /(        *   )       (      (  `    
//   )\()) (   ` )  /( (     )\     )\))(   
//  ((_)\  )\   ( )(_)))\ ((((_)(  ((_)()\  
// __ ((_)((_) (_(_())((_) )\ _ )\ (_()((_) 
// \ \ / / (_) |_   _|| __|(_)_\(_)|  \/  | 
//  \ V /  | | _ | |  | _|  / _ \  | |\/| | 
//   |_|   |_|(_)|_|  |___|/_/ \_\ |_|  |_| 
// 
// This file is subject to the terms and conditions defined in
// file 'License.txt', which is part of this source code package.
// 
// Copyright © Yi.TEAM. All rights reserved.
// -------------------------------------------------------------------------------

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
        /// <param name="enum">枚举值。</param>
        /// <returns>返回 <see cref="EnumExtraAttribute"/> 对象的实例。</returns>
        public static EnumExtraAttribute GetDescription(this Enum @enum)
        {
            return GetDescription<EnumExtraAttribute>(@enum);
        }

        /// <summary>
        /// 返回当前枚举值描述特性的实例。
        /// </summary>
        /// <typeparam name="T">要获取的对象的类型。</typeparam>
        /// <param name="enum">枚举值。</param>
        /// <returns>返回 <typeparamref name="T"/> 对象的实例。</returns>
        public static T GetDescription<T>(this Enum @enum) where T : EnumExtraAttribute
        {
            if (@enum == null) throw new ArgumentNullException(nameof(@enum));

            var enumType = @enum.GetType();
            var attrType = typeof(T);
            var cacheKey = $"{enumType.FullName}`{@enum}`{attrType.FullName}";

            if (_Caches[cacheKey] is T descriptionObject) return descriptionObject;

            var fieldName = @enum.ToString();
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
        /// <param name="enum">枚举值。</param>
        /// <param name="getValueFunction">指定如何获取对象的值。</param>
        /// <param name="defaultValue">在方法返回时，如果没有获取到有效的值，则使用该默认值代替。</param>
        /// <returns>返回 <typeparamref name="TValue"/> 对象。</returns>
        public static TValue GetDescriptionValue<T, TValue>(this Enum @enum,
            Func<T, TValue> getValueFunction,
            TValue defaultValue = default(TValue))
            where T : EnumExtraAttribute
        {
            if (getValueFunction == null) throw new ArgumentNullException(nameof(getValueFunction));

            var description = GetDescription<T>(@enum);

            if (description == null) return defaultValue;

            var value = getValueFunction(description);

            return Equals(value, default(TValue)) ? defaultValue : value;
        }

        /// <summary>
        /// 返回当前枚举值的数字值。
        /// </summary>
        /// <param name="enum">枚举值。</param>
        /// <returns>返回 <see cref="int"/>。</returns>
        public static int ToInt32(this Enum @enum)
        {
            return Convert.ToInt32(@enum);
        }

        /// <summary>
        /// 返回当前枚举值的数字值。
        /// </summary>
        /// <param name="enum">枚举值。</param>
        /// <returns>返回 <see cref="long"/>。</returns>
        public static long ToInt64(this Enum @enum)
        {
            return Convert.ToInt64(@enum);
        }

        public static EnumExtraInfo GetExtra(this Enum @enum)
        {
            var d = GetDescription(@enum);
            var name = @enum.ToString();
            var value = Convert.ToInt64(@enum);

            return d == null ? new EnumExtraInfo(name, name, value) : d.GetExtra(@enum);
        }
    }
}
