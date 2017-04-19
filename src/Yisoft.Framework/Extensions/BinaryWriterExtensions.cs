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

namespace Yisoft.Framework.Extensions
{
	public static class BinaryWriterExtensions
	{
		public static void Write(this BinaryWriter writer, string value, bool endWith0)
		{
			if (!endWith0)
			{
				writer.Write(value);

				return;
			}

			if (!string.IsNullOrEmpty(value)) writer.Write(value.ToCharArray());

			writer.Write((byte) 0);
		}
	}
}
