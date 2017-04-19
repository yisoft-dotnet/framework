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

using Yisoft.Framework.Utilities;

namespace Yisoft.Framework.Extensions
{
	/// <summary>
	/// 为 <see cref="long"/> 对象提供扩展方法。
	/// </summary>
	public static class Int64Extensions
	{
		/// <summary>
		/// 返回当前 <see cref="long"/> 所表示的文件尺寸的带有单位的字符串。
		/// </summary>
		/// <param name="length"><see cref="long"/></param>
		/// <returns>返回表示文件尺寸的带有单位的字符串。</returns>
		public static string ToFileSize(this long length)
		{
			return string.Format(new FileSizeFormatProvider(), "{0:FS2}", length);
		}

		/// <summary>
		/// 返回当前 <see cref="long"/> 所表示的文件尺寸的带有单位的字符串。
		/// </summary>
		/// <param name="length"><see cref="long"/></param>
		/// <param name="format">指定用于 <see cref="FileSizeFormatProvider"/> 的格式字符串。</param>
		/// <returns>返回表示文件尺寸的带有单位的字符串。</returns>
		public static string ToFileSize(this long length, string format)
		{
			return string.Format(new FileSizeFormatProvider(), $"{{0:{format}}}", length);
		}

		/// <summary>
		/// 返回 <see cref="long"/> 的指定进制数的字符串表示。
		/// </summary>
		/// <param name="input">指定一个 <see cref="long"/>。</param>
		/// <param name="baseChars">指定目标进制元字符的字符串序列。</param>
		/// <returns>返回 <see cref="string"/>。</returns>
		public static string ToBase(this long input, string baseChars = MathUtils.ALPHA_NUMERIC62)
		{
			return MathUtils.Int64ToBase(input, baseChars);
		}
	}
}
