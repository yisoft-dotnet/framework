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

namespace Yisoft.Framework
{
	public static class HostingEnvironments
	{
		public const string Local = "local";

		public const string Development = "development";

		public const string Test = "test";

		public const string Staging = "staging";

		public const string Production = "production";

		public static string NormalizeEnvName(string envName, string defaultValue = Local)
		{
			if (string.IsNullOrWhiteSpace(envName)) return defaultValue ?? string.Empty;

			return envName.ToLower();
		}

		public static string Postfix(string input, string envName, string hiddenValue = Local)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));

			var suffix = NormalizeEnvName(envName, hiddenValue);

			suffix = suffix == hiddenValue ? string.Empty : "_" + suffix;

			return $"{input}{suffix}";
		}
	}
}
