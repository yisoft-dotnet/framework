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

using System.Threading;

namespace Yisoft.Framework.Threading
{
	/// <summary>
	/// 表示对象池性能计数器。
	/// </summary>
	public sealed class ObjectPoolDiagnostics
	{
		private int _objectResetFailedCount;
		private int _poolObjectHitCount;
		private int _poolObjectMissCount;
		private int _poolOverflowCount;

		private int _returnedToPoolByRessurectionCount;
		private int _returnedToPoolCount;
		private int _totalInstancesCreated;
		private int _totalInstancesDestroyed;

		/// <summary>
		/// 获取活动对象数。
		/// </summary>
		public int TotalLiveInstancesCount => _totalInstancesCreated - _totalInstancesDestroyed;

		/// <summary>
		/// 获取重置失败的对象数。
		/// </summary>
		public int ObjectResetFailedCount => _objectResetFailedCount;

		/// <summary>
		/// 获取已返回且复活的对象数。
		/// </summary>
		public int ReturnedToPoolByResurrectionCount => _returnedToPoolByRessurectionCount;

		/// <summary>
		/// 获取已命中的对象数。
		/// </summary>
		public int HitCount => _poolObjectHitCount;

		/// <summary>
		/// 获取丢失的对象数。
		/// </summary>
		public int MissCount => _poolObjectMissCount;

		/// <summary>
		/// 获取已创建的对象数
		/// </summary>
		public int TotalInstancesCreated => _totalInstancesCreated;

		/// <summary>
		/// 获取已回收的对象数。
		/// </summary>
		public int TotalInstancesDestroyed => _totalInstancesDestroyed;

		/// <summary>
		/// 获取溢出的对象数。
		/// </summary>
		public int OverflowCount => _poolOverflowCount;

		/// <summary>
		/// 获取已返回的对象数。
		/// </summary>
		public int ReturnedToPoolCount => _returnedToPoolCount;

		internal void IncrementObjectsCreatedCount() { Interlocked.Increment(ref _totalInstancesCreated); }

		internal void IncrementObjectsDestroyedCount() { Interlocked.Increment(ref _totalInstancesDestroyed); }

		internal void IncrementPoolObjectHitCount() { Interlocked.Increment(ref _poolObjectHitCount); }

		internal void IncrementPoolObjectMissCount() { Interlocked.Increment(ref _poolObjectMissCount); }

		internal void IncrementPoolOverflowCount() { Interlocked.Increment(ref _poolOverflowCount); }

		internal void IncrementResetStateFailedCount() { Interlocked.Increment(ref _objectResetFailedCount); }

		internal void IncrementObjectRessurectionCount() { Interlocked.Increment(ref _returnedToPoolByRessurectionCount); }

		internal void IncrementReturnedToPoolCount() { Interlocked.Increment(ref _returnedToPoolCount); }
	}
}
