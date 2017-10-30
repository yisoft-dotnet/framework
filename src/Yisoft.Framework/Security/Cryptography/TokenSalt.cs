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

namespace Yisoft.Framework.Security.Cryptography
{
    public class TokenSalt
    {
        internal TokenSalt(string salt, string data, int timestamp, int versionPosition, int saltPosition, int dataPosition, int dataLength)
        {
            Salt = salt ?? throw new ArgumentNullException(nameof(salt));
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Timestamp = timestamp;
            VersionPosition = versionPosition;
            SaltPosition = saltPosition;
            DataPosition = dataPosition;
            DataLength = dataLength;
        }

        public string Salt { get; }

        public string Data { get; }

        public int Timestamp { get; }

        public int VersionPosition { get; }

        public int SaltPosition { get; }

        public int DataPosition { get; }

        public int DataLength { get; }
    }
}
