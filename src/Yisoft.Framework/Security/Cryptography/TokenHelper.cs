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

namespace Yisoft.Framework.Security.Cryptography
{
    public class TokenHelper
    {
        public static readonly TokenAlgorithm MD5 = new TokenAlgorithm(TokenHashSettings.MD5);

        public static readonly TokenAlgorithm SHA1 = new TokenAlgorithm(TokenHashSettings.SHA1);

        public static readonly TokenAlgorithm SHA256 = new TokenAlgorithm(TokenHashSettings.SHA256);

        public static readonly TokenAlgorithm SHA384 = new TokenAlgorithm(TokenHashSettings.SHA384);

        public static readonly TokenAlgorithm SHA512 = new TokenAlgorithm(TokenHashSettings.SHA512);
    }
}
