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

using System.Collections;
using System.Reflection;

namespace Yisoft.Framework.Collections.Generic
{
	internal class Proxy
	{
		public object Key;
		public ArrayList List;

		public Proxy(IList bla)
		{
			List = new ArrayList(bla);

			var pi = bla.GetType().GetTypeInfo().GetProperty("Key");

			Key = pi.GetValue(bla, null);
		}
	}
}
