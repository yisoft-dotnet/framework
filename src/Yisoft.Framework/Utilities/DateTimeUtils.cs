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

namespace Yisoft.Framework.Utilities
{
	public class DateTimeUtils
	{
		public static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static Func<DateTime> UtcNowFunc = () => DateTime.UtcNow;

		public static DateTime UtcNow => UtcNowFunc();

		public static int ToTimeStamp(DateTime dateTime) { return (int) (dateTime.ToUniversalTime() - UnixEpoch).TotalSeconds; }

		public static DateTime FromTimeStamp(int timestamp) { return UnixEpoch.AddSeconds(timestamp).ToLocalTime(); }
	}
}
