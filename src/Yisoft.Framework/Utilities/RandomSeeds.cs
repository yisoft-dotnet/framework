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

namespace Yisoft.Framework.Utilities
{
    /// <summary>
    /// 为生成随机字符串指定种子类型的枚举值。
    /// </summary>
    [Flags]
    public enum RandomSeeds
    {
        /// <summary>
        /// 不包含任何种子。
        /// </summary>
        [EnumExtra]
        None = 0,

        /// <summary>
        /// 指定随机字符种子包含数字。
        /// </summary>
        [EnumExtra("数字")]
        Number = 1,

        /// <summary>
        /// 指定随机字符种子包含小写字母。
        /// </summary>
        [EnumExtra("小写字母")]
        Lower = 2,

        /// <summary>
        /// 指定随机字符种子包含大写字母。
        /// </summary>
        [EnumExtra("大写字母")]
        Upper = 4,

        /// <summary>
        /// 指定随机字符种子包含标点符号和特殊字符。
        /// </summary>
        [EnumExtra("特殊字符")]
        Symbol = 8,

        /// <summary>
        /// 指定随机字符种子包含自定义字符。
        /// </summary>
        [EnumExtra("自定义字符")]
        Custom = 32,

        /// <summary>
        /// 指定随机字符种子包含小写字母和大写字母。
        /// </summary>
        [EnumExtra("小写字母和大写字母")]
        Letter = Lower | Upper,

        /// <summary>
        /// 指定随机字符种子包含数字、小写字母和大写字母。
        /// </summary>
        [EnumExtra("数字、小写字母和大写字母")]
        Words = Number | Lower | Upper,

        /// <summary>
        /// 指定随机字符种子包含数字、小写字母、大写字母、标点符号、特殊字符和自定义字符。
        /// </summary>
        [EnumExtra("全部")]
        All = Number | Lower | Upper | Symbol | Custom
    }
}
