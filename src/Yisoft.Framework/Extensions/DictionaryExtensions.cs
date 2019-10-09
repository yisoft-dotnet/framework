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
            if (jobject == null) return;
            if (dictionary == null || dictionary.Count == 0) return;

            foreach (var (key, value) in dictionary)
            {
                if (jobject.TryGetValue(key, out _)) throw new Exception($"Item does already exist - cannot add it via a custom entry: {key}");

                jobject.Add(
                    value.GetType().GetTypeInfo().IsClass
                        ? new JProperty(key, JToken.FromObject(value))
                        : new JProperty(key, value)
                );
            }
        }
    }
}