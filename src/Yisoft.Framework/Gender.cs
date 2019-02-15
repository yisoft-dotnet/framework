// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

namespace Yisoft.Framework
{
    /// <summary>
    /// 表示性别的枚举值。
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 保密。
        /// </summary>
        [EnumExtra("保密")]
        Unknown = 0,

        /// <summary>
        /// 男性。
        /// </summary>
        [EnumExtra("先生")]
        Male = 1,

        /// <summary>
        /// 女性。
        /// </summary>
        [EnumExtra("女士")]
        Female = 2
    }
}