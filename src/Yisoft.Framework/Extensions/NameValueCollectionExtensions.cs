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
using System.Collections.Specialized;
using System.Text;

namespace Yisoft.Framework.Extensions
{
	/// <summary>
	/// 为 <see cref="NameValueCollection"/> 对象提供扩展方法。
	/// </summary>
	public static class NameValueCollectionExtensions
	{
		/// <summary>
		/// 从 HTTP QueryString 字符串中读取数据到集合中。
		/// </summary>
		/// <param name="collection"><see cref="NameValueCollection"/>。</param>
		/// <param name="queryString">表示 HTTP QueryString 字符串。</param>
		public static void ReadQueryString(this NameValueCollection collection, string queryString)
		{
			if (queryString == null) throw new ArgumentNullException(nameof(queryString));

			var items = queryString.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries);

			foreach (var item in items)
			{
				var data = item.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);

				if (data.Length == 2) collection.Set(data[0], data[1]);
			}
		}

		/// <summary>
		/// 将当前集合中的键值对转换为 HTTP QueryString 格式。
		/// </summary>
		/// <param name="collection"><see cref="NameValueCollection"/>。</param>
		/// <returns>返回 HTTP QueryString 字符串。</returns>
		public static string ToQueryString(this NameValueCollection collection)
		{
			var queryString = new StringBuilder();

			foreach (var key in collection.AllKeys)
			{
				if (queryString.Length > 0) queryString.Append("&");

				queryString.Append(key);
				queryString.Append("=");
				queryString.Append(collection[key]);
			}

			return queryString.ToString();
		}
	}
}
