// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

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
                if (jobject.TryGetValue(item.Key, out _)) throw new Exception($"Item does already exist - cannot add it via a custom entry: {item.Key}");

                jobject.Add(
                    item.Value.GetType().GetTypeInfo().IsClass
                        ? new JProperty(item.Key, JToken.FromObject(item.Value))
                        : new JProperty(item.Key, item.Value)
                );
            }
        }
    }
}