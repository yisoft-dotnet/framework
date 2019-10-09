// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Yisoft.Framework.Extensions
{
    public static class JsonExtensions
    {
        public static string ToQueryString(this JToken jToken, bool escape = true)
        {
            if (jToken == null) return null;

            var jValues = new SortedList<string, string>();
            var builder = new StringBuilder();

            _FetchValues(jToken, jValues, escape);

            foreach (var p in jValues)
            {
                builder.Append('&');
                builder.Append(p.Key);
                builder.Append('=');
                builder.Append(p.Value);
            }

            return builder.Length > 0 ? builder.Remove(0, 1).ToString() : builder.ToString();
        }

        private static void _FetchValues(JToken jToken, SortedList<string, string> jValues, bool escape)
        {
            foreach (var token in jToken.Children())
            {
                if (!(token is JValue jValue)) _FetchValues(token, jValues, escape);
                else
                {
                    var jvalue = jValue.Value.ToString();

                    if (string.IsNullOrEmpty(jvalue)) continue;

                    var key = escape ? Uri.EscapeUriString(jValue.Path) : jValue.Path;
                    var value = escape ? Uri.EscapeUriString(jvalue) : jvalue;

                    jValues.Add(key, value);
                }
            }
        }

        public static string ToJson(this object obj, Formatting formatting = Formatting.None, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(obj, formatting, settings ?? JsonHelper.DefaultJsonSerializerSettings);
        }
    }
}