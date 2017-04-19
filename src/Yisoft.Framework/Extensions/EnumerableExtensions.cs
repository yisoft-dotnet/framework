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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Yisoft.Framework.Extensions
{
	[DebuggerStepThrough]
	public static class EnumerableExtensions
	{
		public static string Join(this IEnumerable<object> source, string separator = ", ") { return string.Join(separator, source); }

		public static IEnumerable<T> Distinct<T>(
			this IEnumerable<T> source, Func<T, T, bool> comparer)
			where T : class
		{
			return source.Distinct(new DynamicEqualityComparer<T>(comparer));
		}

		private sealed class DynamicEqualityComparer<T> : IEqualityComparer<T>
			where T : class
		{
			private readonly Func<T, T, bool> _func;

			public DynamicEqualityComparer(Func<T, T, bool> func) { _func = func; }

			public bool Equals(T x, T y) { return _func(x, y); }

			public int GetHashCode(T obj)
			{
				return 0; // force Equals
			}
		}
	}
}
