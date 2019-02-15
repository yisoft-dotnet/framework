// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework.Net.Http
{
    public abstract partial class RestfulClientBase
    {
        protected async Task<HttpResponseMessage> GetAsync(
            Uri uri,
            Action<HttpRequestHeaders> addHeadersAction = null)
        {
            return await SendAsync(uri, HttpMethod.Get, null, addHeadersAction, HttpCompletionOption.ResponseHeadersRead);
        }

        protected async Task<T> GetAsync<T>(
            Uri uri,
            Action<HttpRequestHeaders> addHeadersAction = null)
        {
            var response = await GetAsync(uri, addHeadersAction);

            return await response.DeserializeJsonObjectAsync<T>();
        }
    }
}