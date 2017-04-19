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
		public EnumExtraAttribute(string title = null)
		{
			SetDescription(title);
		}

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
		public void SetDescription(string title)
		{
			Title = title;
		}

		public virtual EnumExtraInfo GetExtra(Enum @enum)
		{
			var value = Convert.ToInt32(@enum);

			return new EnumExtraInfo(Title, @enum.ToString(), value, Rank == 0 ? value : Rank);
		}
	}
}
