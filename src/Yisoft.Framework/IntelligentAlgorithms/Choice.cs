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
    public struct Choice<T>
    {
        public readonly ChoiceType Type;
        public readonly T Removed;
        public readonly T Added;

        public bool HasRemoved => Type != ChoiceType.Add;

        public bool HasAdded => Type != ChoiceType.Remove;

        internal Choice(ChoiceType type, T removed, T added)
        {
            Type = type;
            Removed = removed;
            Added = added;
        }

        public static Choice<T> Add(T value) { return new Choice<T>(ChoiceType.Add, default(T), value); }

        public static Choice<T> Remove(T value) { return new Choice<T>(ChoiceType.Remove, value, default(T)); }

        public static Choice<T> Equal(T value) { return new Choice<T>(ChoiceType.Equal, value, value); }

        public static Choice<T> Substitute(T remove, T add) { return new Choice<T>(ChoiceType.Substitute, remove, add); }

        internal static Choice<T> Transpose(T remove, T add) { return new Choice<T>(ChoiceType.Transpose, remove, add); }

        public override string ToString()
        {
            switch (Type)
            {
                case ChoiceType.Equal: return $"{Added}";
                case ChoiceType.Substitute: return $"[-{Removed}+{Added}]";
                case ChoiceType.Remove: return $"-{Removed}";
                case ChoiceType.Add: return $"+{Added}";
                default: return null;
            }
        }
    }
}
