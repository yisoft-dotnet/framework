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

namespace Yisoft.Framework
{
	/// <summary>
	/// 表示十二生肖的枚举值。
	/// </summary>
	public enum ChineseZodiac
	{
		/// <summary>
		/// 未知或未指定。
		/// </summary>
		[EnumExtra]
		Unknown = 0,

		/// <summary>
		/// 鼠。
		/// </summary>
		[EnumExtra("鼠")]
		Rat = 1,

		/// <summary>
		/// 牛。
		/// </summary>
		[EnumExtra("牛")]
		Ox = 2,

		/// <summary>
		/// 虎。
		/// </summary>
		[EnumExtra("虎")]
		Tiger = 3,

		/// <summary>
		/// 兔。
		/// </summary>
		[EnumExtra("兔")]
		Hare = 4,

		/// <summary>
		/// 龙。
		/// </summary>
		[EnumExtra("龙")]
		Dragon = 5,

		/// <summary>
		/// 蛇。
		/// </summary>
		[EnumExtra("蛇")]
		Snake = 6,

		/// <summary>
		/// 马。
		/// </summary>
		[EnumExtra("马")]
		Horse = 7,

		/// <summary>
		/// 羊。
		/// </summary>
		[EnumExtra("羊")]
		Sheep = 8,

		/// <summary>
		/// 猴。
		/// </summary>
		[EnumExtra("猴")]
		Monkey = 9,

		/// <summary>
		/// 鸡。
		/// </summary>
		[EnumExtra("鸡")]
		Cock = 10,

		/// <summary>
		/// 狗。
		/// </summary>
		[EnumExtra("狗")]
		Dog = 11,

		/// <summary>
		/// 猪。
		/// </summary>
		[EnumExtra("猪")]
		Boar = 12
	}
}
