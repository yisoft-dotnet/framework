// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Yisoft.Framework.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static T DeserializeJsonObject<T>(this HttpResponseMessage response)
        {
            if (response == null) return default;

            var data = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(data, JsonHelper.DefaultJsonSerializerSettings);
        }

        public static Task<T> DeserializeJsonObjectAsync<T>(this HttpResponseMessage response)
        {
            if (response == null) return default;

            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<T>(data, JsonHelper.DefaultJsonSerializerSettings);

            return Task.FromResult(obj);
        }
    }
}