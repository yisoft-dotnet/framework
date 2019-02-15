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