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

using Yisoft.Framework.Extensions;

namespace Yisoft.Framework
{
    public class EnumExtraInfo
    {
        public EnumExtraInfo() { }

        public EnumExtraInfo(string title, string name, long value)
            : this(title, name, value, value)
        {
        }

        public EnumExtraInfo(string title, string name, long value, long rank)
        {
            Title = title == name ? title.ToCamelCase() : title;
            Name = name.ToCamelCase();
            Value = value;
            Rank = rank == 0 ? null : Rank;
        }

        public string Title { get; }

        public string Name { get; }

        public long Value { get; }

        public long? Rank { get; }
    }
}
