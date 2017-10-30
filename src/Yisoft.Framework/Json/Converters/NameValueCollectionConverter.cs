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
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Yisoft.Framework.Json.Converters
{
    public class NameValueCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) { return objectType == typeof(NameValueCollection); }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var collection = existingValue as NameValueCollection ?? new NameValueCollection();
            var items = serializer.Deserialize<NameValueCollectionItem[]>(reader);

            if (items == null) return collection;

            foreach (var item in items)
            {
                if (item.Values == null) continue;

                foreach (var value in item.Values) collection.Add(item.Key, value);
            }

            return collection;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var collection = (NameValueCollection) value;

            if (collection == null) return;

            var items = new NameValueCollectionItem[collection.AllKeys.Length];
            var index = 0;

            foreach (var key in collection.AllKeys)
            {
                items[index++] = new NameValueCollectionItem
                {
                    Key = key,
                    Values = collection.GetValues(key)
                };
            }

            serializer.Serialize(writer, items);
        }

        public class NameValueCollectionItem
        {
            public string Key { get; set; }

            public string[] Values { get; set; }
        }
    }
}
