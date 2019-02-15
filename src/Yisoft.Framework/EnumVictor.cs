// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Yisoft.Framework
{
    /// <summary>
    /// 包含一组方法和属性，封装枚举值的自定义属性的基本操作。
    /// </summary>
    /// <typeparam name="T">指定表示枚举值的 <see cref="System.Type"/>。</typeparam>
    public class EnumVictor<T> : EnumVictor<T, EnumExtraAttribute>
    {
        /// <summary>
        /// 初始化 <see cref="EnumVictor&lt;T&gt;"/> 类的新实例。
        /// </summary>
        public EnumVictor()
            : this(null)
        {
        }

        /// <summary>
        /// 使用指定的描述字典初始化 <see cref="EnumVictor&lt;T&gt;"/> 类的新实例。
        /// </summary>
        /// <param name="descriptionFilter">指定描述字典，其键名应当为该枚举值的默认描述的文本。</param>
        public EnumVictor(Dictionary<T, string> descriptionFilter)
            : base(descriptionFilter)
        {
        }
    }

    /// <summary>
    /// 包含一组方法和属性，封装枚举值的自定义属性的基本操作。
    /// </summary>
    /// <typeparam name="TEnum">指定表示枚举值的 <see cref="System.Type"/>。</typeparam>
    /// <typeparam name="TEnumDescription">指定表示枚举值自定描述的 <see cref="EnumExtraAttribute"/>。</typeparam>
    public class EnumVictor<TEnum, TEnumDescription> where TEnumDescription : EnumExtraAttribute
    {
        /// <summary>
        /// 初始化 <see cref="EnumVictor&lt;TEnum, TEnumDescription&gt;"/> 类的新实例。
        /// </summary>
        public EnumVictor()
            : this(null)
        {
        }

        /// <summary>
        /// 使用指定的描述字典初始化 <see cref="EnumVictor&lt;TEnum, TEnumDescription&gt;"/> 类的新实例。
        /// </summary>
        /// <param name="descriptionFilter">指定描述字典，其键名应当为该枚举值的默认描述的文本。</param>
        public EnumVictor(Dictionary<TEnum, string> descriptionFilter)
        {
            EnumType = typeof(TEnum);

            if (EnumType == null || EnumType.GetTypeInfo().IsEnum == false) throw new ArgumentException("参数 T 必须是 System.Enum 类型。");

            FieldInfos = EnumType.GetTypeInfo().GetFields();
            DescriptionFilter = descriptionFilter;
            DefaultDescriptions = new Dictionary<TEnum, TEnumDescription>();
            Descriptions = new Dictionary<TEnum, TEnumDescription>();

            _Initialize();
        }

        /// <summary>
        /// 返回指定枚举值的自定义描述的实例。
        /// </summary>
        /// <param name="enumObj">指定一个枚举项。</param>
        /// <returns>如果获取成功则返回 <see cref="EnumExtraAttribute"/> 的实例，否则返回 null。</returns>
        public TEnumDescription this[TEnum enumObj] => GetDescription(enumObj);

        /// <summary>
        /// 获取自定义描述字典，其键名为当前枚举值的默认描述的文本。本属性可以为 null。
        /// </summary>
        public Dictionary<TEnum, string> DescriptionFilter { get; }

        /// <summary>
        /// 获取指定枚举类型的默认描述的集合。如果没有找到自定义描述信息，将使用其枚举值的字段名称填充该集合。
        /// </summary>
        public Dictionary<TEnum, TEnumDescription> DefaultDescriptions { get; }

        /// <summary>
        /// 获取指定枚举类型的默认描述的集合。如果没有找到自定义描述信息，将使用其枚举值的字段名称填充该集合。本属性包含的描述信息会受到 <see cref="EnumVictor&lt;TEnum, TEnumDescription&gt;.DescriptionFilter"/> 的影响。
        /// </summary>
        public Dictionary<TEnum, TEnumDescription> Descriptions { get; }

        /// <summary>
        /// 获取指定枚举类型的字段元数据的集合。
        /// </summary>
        public FieldInfo[] FieldInfos { get; }

        /// <summary>
        /// 获取指定枚举类型的 <see cref="System.Type"/>。
        /// </summary>
        public Type EnumType { get; }

        private static HashSet<TEnumDescription> _GetBitFieldDescriptions(TEnum enumObj, IDictionary<TEnum, TEnumDescription> descriptions)
        {
            var enumDescriptions = new HashSet<TEnumDescription>();
            var x = Convert.ToInt32(enumObj);

            // 指定枚举值不可再分时直接返回
            if (x == 0 || (x & (x - 1)) == 0)
            {
                if (descriptions.ContainsKey(enumObj)) enumDescriptions.Add(descriptions[enumObj]);

                return enumDescriptions;
            }

            foreach (var item in descriptions)
            {
                var y = Convert.ToInt32(item.Key);

                if (y == 0) continue;
                if ((y & (y - 1)) != 0) continue;
                if ((x & y) != y) continue;

                enumDescriptions.Add(item.Value);
            }

            return enumDescriptions;
        }

        private static string _GetBitFieldDescriptionValue(TEnum enumObj, IDictionary<TEnum, TEnumDescription> descriptions)
        {
            var x = Convert.ToInt32(enumObj);
            var result = new StringBuilder();

            // 指定枚举值不可再分时直接返回
            if (x == 0 || (x & (x - 1)) == 0)
            {
                result.Append(descriptions.ContainsKey(enumObj) ? descriptions[enumObj].Title : enumObj.ToString());

                return result.ToString();
            }

            foreach (var item in descriptions)
            {
                var y = Convert.ToInt32(item.Key);

                if (y == 0) continue;
                if ((y & (y - 1)) != 0) continue;
                if ((x & y) != y) continue;

                if (result.Length == 0) result.Append(item.Value.Title);
                else result.Append(", ").Append(item.Value.Title);
            }

            return result.ToString();
        }

        private void _Initialize()
        {
            var enumValues = Enum.GetValues(EnumType);

            foreach (var value in enumValues)
            {
                if (value is TEnum == false) continue;

                var itemValue = (TEnum) value;
                var descAttribute = _GetDescription(value.ToString());

                if (DescriptionFilter != null && DescriptionFilter.Count > 0 && DescriptionFilter.ContainsKey(itemValue))
                    descAttribute.SetDescription(DescriptionFilter[itemValue]);
                if (Descriptions.ContainsKey(itemValue) == false) Descriptions.Add(itemValue, descAttribute);
                if (DefaultDescriptions.ContainsKey(itemValue) == false) DefaultDescriptions.Add(itemValue, _GetDescription(value.ToString()));
            }
        }

        private TEnumDescription _GetDescription(string value)
        {
            var fieldName = value;
            var fieldInfo = EnumType.GetTypeInfo().GetField(fieldName);
            var attributes = (TEnumDescription[]) fieldInfo.GetCustomAttributes(typeof(TEnumDescription), false);

            return attributes.Length <= 0
                ? (TEnumDescription) Activator.CreateInstance(typeof(TEnumDescription), fieldName)
                : attributes[0];
        }

        /// <summary>
        /// 返回与指定自定义描述字符串相匹配的枚举值。
        /// </summary>
        /// <param name="descriptionValue">表示枚举值自定义描述的 <see cref="string"/>。</param>
        /// <param name="ignoreCase">指示所进行的比较是否区分大小写。（true 指示所进行的比较不区分大小写。）</param>
        /// <param name="canFilter">指定是否允许从自定义描述字典中查找匹配项。</param>
        /// <returns>返回 <typeparamref name="TEnum"/>。</returns>
        public TEnum GetEnumFromDescription(string descriptionValue, bool ignoreCase = false, bool canFilter = true)
        {
            if (string.IsNullOrEmpty(descriptionValue)) return default(TEnum);
            if (Descriptions.Count == 0) return default(TEnum);

            var list = canFilter ? Descriptions.Select(item => item) : DefaultDescriptions.Select(item => item);

            foreach (var item in list)
                if (string.Compare(item.Value.Title, descriptionValue, ignoreCase) == 0)
                    return item.Key;

            return default(TEnum);
        }

        /// <summary>
        /// 返回指定枚举值的自定义描述的实例。
        /// </summary>
        /// <param name="enumObj">指定一个枚举项。</param>
        /// <param name="canFilter">指定是否允许从自定义描述字典中返回值。</param>
        /// <returns>如果获取成功则返回 <see cref="EnumExtraAttribute"/> 的实例，否则返回 null。</returns>
        public TEnumDescription GetDescription(TEnum enumObj, bool canFilter = true)
        {
            return canFilter
                ? (Descriptions.ContainsKey(enumObj) ? Descriptions[enumObj] : null)
                : (DefaultDescriptions.ContainsKey(enumObj) ? DefaultDescriptions[enumObj] : null);
        }

        /// <summary>
        /// 返回指定枚举值的自定义描述的文本。
        /// </summary>
        /// <param name="enumObj">指定一个枚举项。</param>
        /// <param name="canFilter">指定是否允许从自定义描述字典中返回值。</param>
        /// <param name="defaultValue">指定一个字符串，如果找不到自定义描述信息时将其作为默认值返回。</param>
        /// <returns>如果获取成功则返回枚举值的自定义描述的文本；如果没有找到自定义描述信息且指定了 <paramref name="defaultValue"/> 的值则<br />
        /// 返回 <paramref name="defaultValue"/>，否则返回 <paramref name="enumObj"/>。</returns>
        public string GetDescriptionValue(TEnum enumObj, bool canFilter = false, string defaultValue = null)
        {
            return canFilter
                ? (Descriptions.ContainsKey(enumObj) ? Descriptions[enumObj].Title : defaultValue ?? enumObj.ToString())
                : (DefaultDescriptions.ContainsKey(enumObj) ? DefaultDescriptions[enumObj].Title : defaultValue ?? enumObj.ToString());
        }

        /// <summary>
        /// 返回指定位域（包含 <see cref="FlagsAttribute"/> 属性的枚举值）的自定义描述的文本。
        /// </summary>
        /// <param name="enumObj">指定一个枚举项。</param>
        /// <param name="canFilter">指定是否允许从自定义描述字典中返回值。</param>
        /// <param name="defaultValue">指定一个字符串，如果找不到自定义描述信息时将其作为默认值返回。</param>
        /// <returns>如果获取成功则返回枚举值的自定义描述的文本；如果没有找到自定义描述信息且指定了 <paramref name="defaultValue"/> 的值则<br />
        /// 返回 <paramref name="defaultValue"/>，否则返回 <paramref name="enumObj"/>。</returns>
        public string GetBitFieldDescriptionValue(TEnum enumObj, bool canFilter = false, string defaultValue = null)
        {
            var result = _GetBitFieldDescriptionValue(enumObj, canFilter ? Descriptions : DefaultDescriptions);

            return result ?? defaultValue ?? enumObj.ToString();
        }

        /// <summary>
        /// 返回指定位域（包含 <see cref="FlagsAttribute"/> 属性的枚举值）的自定义描述的集合。
        /// </summary>
        /// <param name="enumObj">指定一个枚举项。</param>
        /// <param name="canFilter">指定是否允许从自定义描述字典中返回值。</param>
        /// <returns>如果获取成功则返回枚举值的自定义描述的集合；否则返回空集合。</returns>
        public HashSet<TEnumDescription> GetBitFieldDescriptions(TEnum enumObj, bool canFilter = false)
        {
            return _GetBitFieldDescriptions(enumObj, canFilter ? Descriptions : DefaultDescriptions);
        }

        /// <summary>
        /// 返回枚举值的自定义描述的文本的数组。
        /// </summary>
        /// <param name="canFilter">指定是否允许从自定义描述字典中返回值。</param>
        /// <returns>返回包含枚举值的自定义描述的文本的 <see cref="System.Array"/>。</returns>
        public List<KeyValuePair<TEnum, string>> GetDescriptionValues(bool canFilter = false)
        {
            var list = canFilter ? Descriptions.Select(item => item) : DefaultDescriptions.Select(item => item);

            return new List<KeyValuePair<TEnum, string>>(list.Select(item => new KeyValuePair<TEnum, string>(item.Key, item.Value.Title)));
        }
    }
}