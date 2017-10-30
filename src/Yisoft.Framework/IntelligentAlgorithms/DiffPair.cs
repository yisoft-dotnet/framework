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

namespace Yisoft.Framework.IntelligentAlgorithms
{
    public struct DiffPair<T>
    {
        public DiffPair(DiffAction action, T value)
        {
            Action = action;
            Value = value;
        }

        public readonly DiffAction Action;
        public readonly T Value;

        public override string ToString()
        {
            var str = Action == DiffAction.Added ? "+" : Action == DiffAction.Removed ? "-" : string.Empty;

            return str + Value;
        }
    }
}
