// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

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
        protected Task<T> PatchAsync<T>(
            Uri uri,
            object data,
            Action<HttpRequestHeaders> addHeadersAction = null)
        {
            return PatchAsync<T>(uri, data, Encoding.UTF8, addHeadersAction);
        }

        protected async Task<T> PatchAsync<T>(
            Uri uri,
            object data,
            Encoding encoding,
            Action<HttpRequestHeaders> addHeadersAction = null)
        {
            var response = await PatchAsync(uri, data, encoding, addHeadersAction);

            return await response.DeserializeJsonObjectAsync<T>();
        }

        protected async Task<T> PatchAsync<T>(
            Uri uri,
            HttpContent content,
            Action<HttpRequestHeaders> addHeadersAction = null)
        {
            var response = await PatchAsync(uri, content, addHeadersAction);

            return await response.DeserializeJsonObjectAsync<T>();
        }

        protected Task<HttpResponseMessage> PatchAsync(
            Uri uri,
            object data,
            Action<HttpRequestHeaders> addHeadersAction = null)
        {
            return PatchAsync(uri, data, Encoding.UTF8, addHeadersAction);
        }

        protected async Task<HttpResponseMessage> PatchAsync(
            Uri uri,
            object data,
            Encoding encoding,
            Action<HttpRequestHeaders> addHeadersAction = null)
        {
            return await SendAsync(uri, new HttpMethod("PATCH"), data, encoding, addHeadersAction);
        }

        protected async Task<HttpResponseMessage> PatchAsync(
            Uri uri,
            HttpContent content,
            Action<HttpRequestHeaders> addHeadersAction = null)
        {
            return await SendAsync(uri, new HttpMethod("PATCH"), content, addHeadersAction);
        }
    }
}