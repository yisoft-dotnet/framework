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

using System.IO;
using System.Text;

namespace Yisoft.Framework.Extensions
{
    public static class BinaryReaderExtenions
    {
        public static string ReadString(this BinaryReader reader, bool endWith0)
        {
            if (!endWith0) return reader.ReadString();

            var contentBuilder = new StringBuilder();

            char c;

            while ((c = reader.ReadChar()) > char.MinValue) contentBuilder.Append(c);

            return contentBuilder.ToString();
        }
    }
}
