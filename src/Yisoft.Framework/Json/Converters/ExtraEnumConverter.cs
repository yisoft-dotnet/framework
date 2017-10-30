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
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework.Json.Converters
{
    public class ExtraEnumConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();

                return;
            }

            var e = (Enum) value;
            var enumValue = Convert.ToInt64(e);
            var enumName = e.ToString("G");

            var extra = e.GetExtra() ?? new EnumExtraInfo(enumName, enumName, enumValue);

            writer.WriteStartObject();

            writer.WritePropertyName(_FormatText(nameof(extra.Title)));
            writer.WriteValue(_FormatText(extra.Title));

            writer.WritePropertyName(_FormatText(nameof(extra.Name)));
            writer.WriteValue(_FormatText(extra.Name));

            writer.WritePropertyName(_FormatText(nameof(extra.Value)));
            writer.WriteValue(extra.Value);

            writer.WritePropertyName(_FormatText(nameof(extra.Rank)));
            writer.WriteValue(extra.Rank ?? 0);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject) return base.ReadJson(reader, objectType, existingValue, serializer);

            string enumName = null;
            long? enumValue = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject) break;
                if (reader.TokenType != JsonToken.PropertyName) continue;

                switch (reader.Value.ToString().ToUpper())
                {
                    case "NAME":
                        enumName = reader.ReadAsString();

                        continue;
                    case "VALUE":
                        enumValue = long.TryParse(reader.ReadAsString(), out var value) ? (long?) value : null;
                        break;
                }
            }

            var isNullable = objectType.GetTypeInfo().IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);
            var enumType = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            if (enumName != null) return Enum.Parse(enumType, enumName, true);

            return enumValue == null ? null : Enum.ToObject(enumType, enumValue);
        }

        private string _FormatText(string text)
        {
            if (text == null) return null;

            return CamelCaseText ? text.ToCamelCase() : text;
        }
    }
}
