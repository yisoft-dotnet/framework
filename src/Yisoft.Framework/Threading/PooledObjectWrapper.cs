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

namespace Yisoft.Framework.Threading
{
	/// <summary>
	/// 包装对象池中的对象资源。
	/// </summary>
	/// <typeparam name="T">对象资源的类型。</typeparam>
	public class PooledObjectWrapper<T> : PooledObject
	{
		/// <summary>
		/// 初始化 <see cref="PooledObjectWrapper{T}"/> 类的新实例。
		/// </summary>
		/// <param name="resource">对象资源的实例。</param>
		public PooledObjectWrapper(T resource)
		{
			if (Equals(resource, default(T))) throw new ArgumentException("resource cannot be null");

			InternalResource = resource;
		}

		/// <summary>
		/// 在释放对象的时候发生。
		/// </summary>
		public Action<T> WrapperReleaseResourcesAction { get; set; }

		/// <summary>
		/// 在重置对象状态的时候发生。
		/// </summary>
		public Action<T> WrapperResetStateAction { get; set; }

		/// <summary>
		/// 获取已包装的对象资源。
		/// </summary>
		public T InternalResource { get; }

		/// <summary>
		/// 在释放对象时发生。
		/// </summary>
		protected override void OnRelease()
		{
			WrapperReleaseResourcesAction?.Invoke(InternalResource);
		}

		/// <summary>
		/// 在重置对象状态时发生。
		/// </summary>
		protected override void OnReset()
		{
			WrapperResetStateAction?.Invoke(InternalResource);
		}
	}
}
