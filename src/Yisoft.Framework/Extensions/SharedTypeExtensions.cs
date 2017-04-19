﻿//      )                             *     
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
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Yisoft.Framework.Extensions
{
	[DebuggerStepThrough]
	public static class SharedTypeExtensions
	{
		public static Type UnwrapNullableType(this Type type) => Nullable.GetUnderlyingType(type) ?? type;

		public static bool IsNullableType(this Type type)
		{
			var typeInfo = type.GetTypeInfo();

			return !typeInfo.IsValueType || (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		public static Type MakeNullable(this Type type) => type.IsNullableType() ? type : typeof(Nullable<>).MakeGenericType(type);

		public static bool IsInteger(this Type type)
		{
			type = type.UnwrapNullableType();

			return type == typeof(int)
			       || type == typeof(long)
			       || type == typeof(short)
			       || type == typeof(byte)
			       || type == typeof(uint)
			       || type == typeof(ulong)
			       || type == typeof(ushort)
			       || type == typeof(sbyte);
		}

		public static bool IsIntegerForSerial(this Type type)
		{
			type = type.UnwrapNullableType();

			return type == typeof(int)
			       || type == typeof(long)
			       || type == typeof(short);
		}

		public static PropertyInfo GetAnyProperty(this Type type, string name)
		{
			var props = type.GetRuntimeProperties().Where(p => p.Name == name).ToList();

			if (props.Count > 1) throw new AmbiguousMatchException();

			return props.SingleOrDefault();
		}

		private static bool _IsNonIntegerPrimitive(this Type type)
		{
			type = type.UnwrapNullableType();

			return type == typeof(bool)
			       || type == typeof(byte[])
			       || type == typeof(char)
			       || type == typeof(DateTime)
			       || type == typeof(DateTimeOffset)
			       || type == typeof(decimal)
			       || type == typeof(double)
			       || type == typeof(float)
			       || type == typeof(Guid)
			       || type == typeof(string)
			       || type == typeof(TimeSpan)
			       || type.GetTypeInfo().IsEnum;
		}

		public static bool IsPrimitive(this Type type) => type.IsInteger() || type._IsNonIntegerPrimitive();

		public static Type UnwrapEnumType(this Type type) => type.GetTypeInfo().IsEnum ? Enum.GetUnderlyingType(type) : type;
	}
}
