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

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Yisoft.Framework.Extensions
{
	public static class HttpResponseMessageExtensions
	{
		public static T DeserializeJsonObject<T>(this HttpResponseMessage response)
		{
			var data = response.Content.ReadAsStringAsync().Result;

			return JsonConvert.DeserializeObject<T>(data, JsonHelper.DefaultJsonSerializerSettings);
		}

		public static async Task<T> DeserializeJsonObjectAsync<T>(this HttpResponseMessage response)
		{
			var data = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(data, JsonHelper.DefaultJsonSerializerSettings);
		}
	}
}
