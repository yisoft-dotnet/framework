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
	/// 表示十二星座的枚举值。
	/// </summary>
	public enum Constellation
	{
		/// <summary>
		/// 未知或未指定。
		/// </summary>
		[ConstellationDescription("未指定", "Unknown", 0, 0, 0, 0)]
		Unknown = 0,

		/// <summary>
		/// 魔羯座 (12月22日 - 01月19日)。
		/// </summary>
		[ConstellationDescription("魔羯座", "Capricorn", 12, 22, 1, 19)]
		Capricorn = 1,

		/// <summary>
		/// 水瓶座 (01月20日 - 02月18日)。
		/// </summary>
		[ConstellationDescription("水瓶座", "Aquarius", 1, 20, 2, 18)]
		Aquarius = 2,

		/// <summary>
		/// 双鱼座 (02月19日 - 03月20日)。
		/// </summary>
		[ConstellationDescription("双鱼座", "Pisces", 2, 19, 3, 20)]
		Pisces = 3,

		/// <summary>
		/// 白羊座 (03月21日 - 04月20日)。
		/// </summary>
		[ConstellationDescription("白羊座", "Aries", 3, 21, 4, 20)]
		Aries = 4,

		/// <summary>
		/// 金牛座 (04月21日 - 05月20日)。
		/// </summary>
		[ConstellationDescription("金牛座", "Taurus", 4, 21, 5, 20)]
		Taurus = 5,

		/// <summary>
		/// 双子座 (05月21日 - 06月21日)。
		/// </summary>
		[ConstellationDescription("双子座", "Gemini", 5, 21, 6, 21)]
		Gemini = 6,

		/// <summary>
		/// 巨蟹座 (06月22日 - 07月22日)。
		/// </summary>
		[ConstellationDescription("巨蟹座", "Cancer", 6, 22, 7, 22)]
		Cancer = 7,

		/// <summary>
		/// 狮子座 (07月23日 - 08月22日)。
		/// </summary>
		[ConstellationDescription("狮子座", "Leo", 7, 23, 8, 22)]
		Leo = 8,

		/// <summary>
		/// 处女座 (08月23日 - 09月22日)。
		/// </summary>
		[ConstellationDescription("处女座", "Virgo", 8, 23, 9, 22)]
		Virgo = 9,

		/// <summary>
		/// 天秤座 (09月23日 - 10月22日)。
		/// </summary>
		[ConstellationDescription("天秤座", "Libra", 9, 23, 10, 22)]
		Libra = 10,

		/// <summary>
		/// 天蝎座 (10月23日 - 11月21日)。
		/// </summary>
		[ConstellationDescription("天蝎座", "Scorpio", 10, 23, 11, 21)]
		Scorpio = 11,

		/// <summary>
		/// 射手座 (11月22日 - 12月21日)。
		/// </summary>
		[ConstellationDescription("射手座", "Sagittarius", 11, 22, 12, 21)]
		Sagittarius = 12
	}
}
