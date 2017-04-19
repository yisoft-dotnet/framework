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
using System.Text;
using System.Threading.Tasks;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework.Net.Http
{
	public abstract partial class RestfulClientBase
	{
		protected Task<T> PutAsync<T>(
			Uri uri,
			object data,
			Action<HttpRequestHeaders> addHeadersAction = null)
		{
			return PutAsync<T>(uri, data, Encoding.UTF8, addHeadersAction);
		}

		protected async Task<T> PutAsync<T>(
			Uri uri,
			object data,
			Encoding encoding,
			Action<HttpRequestHeaders> addHeadersAction = null)
		{
			var response = await PutAsync(uri, data, encoding, addHeadersAction);

			return await response.DeserializeJsonObjectAsync<T>();
		}

		protected async Task<T> PutAsync<T>(
			Uri uri,
			HttpContent content,
			Action<HttpRequestHeaders> addHeadersAction = null)
		{
			var response = await PutAsync(uri, content, addHeadersAction);

			return await response.DeserializeJsonObjectAsync<T>();
		}

		protected Task<HttpResponseMessage> PutAsync(
			Uri uri,
			object data,
			Action<HttpRequestHeaders> addHeadersAction = null)
		{
			return PutAsync(uri, data, Encoding.UTF8, addHeadersAction);
		}

		protected async Task<HttpResponseMessage> PutAsync(
			Uri uri,
			object data,
			Encoding encoding,
			Action<HttpRequestHeaders> addHeadersAction = null)
		{
			return await SendAsync(uri, HttpMethod.Put, data, encoding, addHeadersAction);
		}

		protected async Task<HttpResponseMessage> PutAsync(
			Uri uri,
			HttpContent content,
			Action<HttpRequestHeaders> addHeadersAction = null)
		{
			return await SendAsync(uri, HttpMethod.Put, content, addHeadersAction);
		}
	}
}
