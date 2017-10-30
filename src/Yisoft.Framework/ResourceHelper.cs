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
using System.IO;
using System.Reflection;
using System.Text;

namespace Yisoft.Framework
{
    /// <summary>
    /// 访问程序集资源的帮助类。
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// 返回指定程序集中具有指定名称的 <see cref="Stream"/>。
        /// </summary>
        /// <param name="resName"><see cref="Stream"/> 的名称。</param>
        /// <param name="asm">要从中获取 <see cref="Stream"/> 的 <see cref="Assembly"/> 的实例。</param>
        /// <returns><see cref="Stream"/>。</returns>
        public static Stream GetStreamFromAssembly(string resName, Assembly asm = null)
        {
            if (asm == null) asm = Assembly.GetEntryAssembly();
            if (resName == null) throw new ArgumentNullException(nameof(resName));
            if (resName.Length == 0) throw new ArgumentOutOfRangeException(nameof(resName));

            return asm.GetManifestResourceStream($"{asm.GetName().Name}.{resName}");
        }

        /// <summary>
        /// 返回指定程序集中具有指定名称的资源所包含的字符串。
        /// </summary>
        /// <param name="resName"><see cref="Stream"/> 的名称。</param>
        /// <param name="asm">要从中获取 <see cref="Stream"/> 的 <see cref="Assembly"/> 的实例。</param>
        /// <returns><see cref="Stream"/>。</returns>
        public static string GetStringFromAssembly(string resName, Assembly asm = null)
        {
            string output = null;

            using (var stream = GetStreamFromAssembly(resName, asm))
            {
                using (var streamReader = new StreamReader(stream, Encoding.UTF8, true, 4096))
                {
                    while (streamReader.EndOfStream == false)
                    {
                        var dataLine = streamReader.ReadToEnd();

                        output = string.IsNullOrWhiteSpace(dataLine) ? string.Empty : dataLine;
                    }
                }
            }

            return output;
        }
    }
}
