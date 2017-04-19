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

using System.Collections;
using System.Collections.Generic;

namespace Yisoft.Framework.Utilities
{
	public class Hasher
	{
		private int _hashCode;

		public Hasher() { _hashCode = 17; }

		public Hasher(int seed) { _hashCode = seed; }

		public override int GetHashCode()
		{
			// ReSharper disable once NonReadonlyMemberInGetHashCode
			return _hashCode;
		}

		public Hasher Hash(bool obj)
		{
			_hashCode = 37 * _hashCode + obj.GetHashCode();

			return this;
		}

		public Hasher Hash(int obj)
		{
			_hashCode = 37 * _hashCode + obj.GetHashCode();

			return this;
		}

		public Hasher Hash(long obj)
		{
			_hashCode = 37 * _hashCode + obj.GetHashCode();

			return this;
		}

		public Hasher Hash<T>(T? obj) where T : struct
		{
			_hashCode = 37 * _hashCode + (obj?.GetHashCode() ?? -1);


			return this;
		}

		public Hasher Hash(object obj)
		{
			_hashCode = 37 * _hashCode + (obj?.GetHashCode() ?? -1);

			return this;
		}

		public Hasher HashElements(IEnumerable sequence)
		{
			if (sequence == null)
			{
				_hashCode = 37 * _hashCode + -1;
			}
			else
			{
				foreach (var value in sequence)
				{
					_hashCode = 37 * _hashCode + (value?.GetHashCode() ?? -1);
				}
			}

			return this;
		}

		public Hasher HashStructElements<T>(IEnumerable<T> sequence) where T : struct
		{
			foreach (var value in sequence)
			{
				_hashCode = 37 * _hashCode + value.GetHashCode();
			}

			return this;
		}
	}
}
