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

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Yisoft.Framework.Collections.Generic
{
	[DebuggerDisplay("Key = {Key}  Count = {Count}")]
	[DebuggerTypeProxy(typeof(Proxy))]
	public class Grouping<TKey, T> : List<T>, IGrouping<TKey, T>
	{
		public Grouping(TKey key) { Key = key; }

		public Grouping(TKey key, IEnumerable<T> values)
		{
			Key = key;

			AddRange(values);
		}

		public TKey Key { get; }
	}
}
