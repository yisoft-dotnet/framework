// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework
{
    /// <summary>
    /// 指定枚举值的扩展信息。
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumExtraAttribute : Attribute
    {
        /// <summary>
        /// 初始化 <see cref="EnumExtraAttribute"/> 类的新实例并带有说明。
        /// </summary>
        /// <param name="title">说明文本。</param>
        public EnumExtraAttribute(string title = null) { SetDescription(title); }

        public string Title { get; set; }

        public int Rank { get; set; }

        /// <summary>
        /// 获取或设置用户自定义内容。
        /// </summary>
        public object[] UserOptions { get; set; }

        /// <summary>
        /// 设置说明文本为指定值。
        /// </summary>
        /// <param name="title">说明文本。</param>
        public void SetDescription(string title) { Title = title; }

        public virtual EnumExtraInfo GetExtra(Enum enumObj)
        {
            var value = enumObj.ToInt64();

            return new EnumExtraInfo(Title, enumObj?.ToString(), value, Rank == 0 ? value : Rank);
        }
    }
}