// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;

namespace Yisoft.Framework
{
    /// <summary>
    /// 指定枚举值 <see cref="Constellation"/> 的说明。
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class ConstellationDescriptionAttribute : EnumExtraAttribute
    {
        /// <summary>
        /// 初始化 <see cref="ConstellationDescriptionAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="name">星座名称。</param>
        /// <param name="spell">星座拼写。</param>
        /// <param name="startMonth">星座起始月份。</param>
        /// <param name="startDay">星座起始日期。</param>
        /// <param name="endMonth">星座结束月份。</param>
        /// <param name="endDay">星座结束日期。</param>
        public ConstellationDescriptionAttribute(string name, string spell, int startMonth, int startDay, int endMonth, int endDay)
            : base(name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentOutOfRangeException(nameof(name));
            if (string.IsNullOrWhiteSpace(spell)) throw new ArgumentOutOfRangeException(nameof(spell));

            if (startMonth < 0 || startMonth > 12) throw new ArgumentOutOfRangeException(nameof(startMonth));
            if (startDay < 0 || startDay > 31) throw new ArgumentOutOfRangeException(nameof(startDay));

            if (endMonth < 0 || endMonth > 12) throw new ArgumentOutOfRangeException(nameof(spell));
            if (endDay < 0 || endDay > 31) throw new ArgumentOutOfRangeException(nameof(endDay));

            Name = name;
            Spell = spell;
            StartMonth = startMonth;
            StartDay = startDay;
            EndMonth = endMonth;
            EndDay = endDay;
        }

        /// <summary>
        /// 获取星座名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取星座拼写。
        /// </summary>
        public string Spell { get; }

        /// <summary>
        /// 获取起始月份。
        /// </summary>
        public int StartMonth { get; set; }

        /// <summary>
        /// 获取起始日期。
        /// </summary>
        public int StartDay { get; set; }

        /// <summary>
        /// 获取结束月份。
        /// </summary>
        public int EndMonth { get; set; }

        /// <summary>
        /// 获取结束日期。
        /// </summary>
        public int EndDay { get; set; }
    }
}