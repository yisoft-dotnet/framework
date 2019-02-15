// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework.Net.Http
{
    public abstract partial class RestfulClientBase
    {
        private readonly Uri _baseAddress;

        protected RestfulClientBase(Uri baseAddress) { _baseAddress = baseAddress; }

        public virtual async Task<HttpClient> CreateHttpClient()
        {
            return await Task.FromResult(new HttpClient
            {
                BaseAddress = _baseAddress
            });
        }

        public virtual Uri CreateUri(string path)
        {
            return new UriBuilder(_baseAddress)
            {
                Path = path
            }.Uri;
        }

        public virtual Uri CreateUri(string path, object data)
        {
            var query = JObject.FromObject(data);
            var queryString = query?.ToQueryString();

            return CreateUri(path, queryString);
        }

        public virtual Uri CreateUri(string path, JObject query)
        {
            var queryString = query.ToQueryString();

            return CreateUri(path, queryString);
        }

        public virtual Uri CreateUri(string path, string query)
        {
            return new UriBuilder(_baseAddress)
            {
                Path = path,
                Query = query
            }.Uri;
        }

        protected async Task<HttpResponseMessage> SendAsync(
            Uri uri,
            HttpMethod method,
            object data,
            Action<HttpRequestHeaders> addHeadersAction = null,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            return await SendAsync(uri, method, data, Encoding.UTF8, addHeadersAction, completionOption);
        }

        protected async Task<HttpResponseMessage> SendAsync(
            Uri uri,
            HttpMethod method,
            object data,
            Encoding encoding,
            Action<HttpRequestHeaders> addHeadersAction = null,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = method
            };

            if (data != null)
            {
                var json = data.ToJson();

                requestMessage.Content = new StringContent(json, encoding, "application/json");
            }

            addHeadersAction?.Invoke(requestMessage.Headers);

            return await SendAsync(requestMessage, completionOption);
        }

        protected async Task<HttpResponseMessage> SendAsync(
            Uri uri,
            HttpMethod method,
            HttpContent content,
            Action<HttpRequestHeaders> addHeadersAction = null,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = method
            };

            if (content != null) requestMessage.Content = content;

            addHeadersAction?.Invoke(requestMessage.Headers);

            return await SendAsync(requestMessage, completionOption);
        }

        protected async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            HttpCompletionOption completionOption)
        {
            return await SendAsync(request, completionOption, CancellationToken.None);
        }

        protected virtual async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            using (var client = await CreateHttpClient())
            {
                var response = await client.SendAsync(request, completionOption, cancellationToken);

                if (response.IsSuccessStatusCode) OnSuccess(response);
                else OnFailure(response);

                return response;
            }
        }

        protected virtual void OnFailure(HttpResponseMessage response)
        {
            var message = $"{(int) response.StatusCode}, {response.ReasonPhrase}";

            throw new HttpRequestException(message);
        }

        protected virtual void OnSuccess(HttpResponseMessage response) { }
    }
}