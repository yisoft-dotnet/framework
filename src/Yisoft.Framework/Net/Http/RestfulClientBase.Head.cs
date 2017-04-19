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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework.Net.Http
{
	public abstract partial class RestfulClientBase
	{
		protected async Task<HttpResponseMessage> HeadAsync(
			Uri uri,
			Action<HttpRequestHeaders> addHeadersAction = null)
		{
			return await SendAsync(uri, HttpMethod.Head, null, addHeadersAction, HttpCompletionOption.ResponseHeadersRead);
		}

		protected async Task<T> HeadAsync<T>(
			Uri uri,
			Action<HttpRequestHeaders> addHeadersAction = null)
		{
			var response = await HeadAsync(uri, addHeadersAction);

			return await response.DeserializeJsonObjectAsync<T>();
		}
	}
}
