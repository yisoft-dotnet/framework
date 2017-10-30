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

using System.Net;

namespace Yisoft.Framework.Extensions
{
    /// <summary>
    /// 为 <see cref="IPAddress"/> 对象提供扩展方法。
    /// </summary>
    public static class IPAddressExtensions
    {
        /// <summary>
        /// 返回表示当前 <see cref="System.Net.IPAddress"/> 的字符串。
        /// </summary>
        /// <param name="ipAddress"><see cref="System.Net.IPAddress"/></param>
        /// <param name="format">指定用于 <see cref="IPAddressFormatProvider"/> 的格式字符串。</param>
        /// <returns>返回表示当前 <see cref="System.Net.IPAddress"/> 的字符串。</returns>
        public static string ToString(this IPAddress ipAddress, string format)
        {
            return string.Format(new IPAddressFormatProvider(), $"{{0:{format}}}", ipAddress);
        }
    }
}
