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
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Yisoft.Framework.Extensions
{
	public static class DictionaryExtensions
	{
		public static void AddDictionary(this JObject jobject, Dictionary<string, object> dictionary)
		{
			foreach (var item in dictionary)
			{
				JToken token;

				if (jobject.TryGetValue(item.Key, out token)) throw new Exception("Item does already exist - cannot add it via a custom entry: " + item.Key);

				jobject.Add(
					item.Value.GetType().GetTypeInfo().IsClass
						? new JProperty(item.Key, JToken.FromObject(item.Value))
						: new JProperty(item.Key, item.Value)
				);
			}
		}
	}
}
