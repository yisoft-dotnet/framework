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
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Yisoft.Framework.Threading
{
	/// <summary>
	/// 表示一个对象池。
	/// </summary>
	/// <typeparam name="T">对象资源的类型。</typeparam>
	public class ObjectPool<T> where T : PooledObject
	{
		private const int _DEFAULT_POOL_MINIMUM_SIZE = 5;
		private const int _DEFAULT_POOL_MAXIMUM_SIZE = 100;

		private int _adjustPoolSizeIsInProgressCasFlag;
		private Func<T> _factoryMethod;

		private int _maximumPoolSize;
		private int _minimumPoolSize;
		private ConcurrentQueue<T> _pooledObjects;
		private Action<PooledObject, bool> _returnToPoolAction;

		/// <summary>
		/// 初始化 <see cref="ObjectPool{T}"/> 类的新实例。
		/// </summary>
		public ObjectPool()
		{
			_InitializePool(_DEFAULT_POOL_MINIMUM_SIZE, _DEFAULT_POOL_MAXIMUM_SIZE, null);
		}

		/// <summary>
		/// 初始化 <see cref="ObjectPool{T}"/> 类的新实例。
		/// </summary>
		/// <param name="minimumPoolSize">对象池的最小容量。</param>
		/// <param name="maximumPoolSize">对象池的最大容量。</param>
		public ObjectPool(int minimumPoolSize, int maximumPoolSize)
		{
			_InitializePool(minimumPoolSize, maximumPoolSize, null);
		}

		/// <summary>
		/// 初始化 <see cref="ObjectPool{T}"/> 类的新实例。
		/// </summary>
		/// <param name="factoryMethod">创建对象实例的方法。</param>
		public ObjectPool(Func<T> factoryMethod)
		{
			_InitializePool(_DEFAULT_POOL_MINIMUM_SIZE, _DEFAULT_POOL_MAXIMUM_SIZE, factoryMethod);
		}

		/// <summary>
		/// 初始化 <see cref="ObjectPool{T}"/> 类的新实例。
		/// </summary>
		/// <param name="minimumPoolSize">对象池的最小容量。</param>
		/// <param name="maximumPoolSize">对象池的最大容量。</param>
		/// <param name="factoryMethod">创建对象实例的方法。</param>
		public ObjectPool(int minimumPoolSize, int maximumPoolSize, Func<T> factoryMethod)
		{
			_InitializePool(minimumPoolSize, maximumPoolSize, factoryMethod);
		}

		/// <summary>
		/// 获取对象池性能计数器的实例。
		/// </summary>
		public ObjectPoolDiagnostics Diagnostics { get; private set; }

		/// <summary>
		/// 获取对象数量。
		/// </summary>
		public int ObjectsCount => _pooledObjects.Count;

		/// <summary>
		/// 获取对象池的最小容量。
		/// </summary>
		public int MinimumPoolSize
		{
			get => _minimumPoolSize;
			set
			{
				_ValidatePoolLimits(value, _maximumPoolSize);

				_minimumPoolSize = value;

				_AdjustPoolSizeToBounds();
			}
		}

		/// <summary>
		/// 获取对象池的最大容量。
		/// </summary>
		public int MaximumPoolSize
		{
			get => _maximumPoolSize;
			set
			{
				_ValidatePoolLimits(_minimumPoolSize, value);

				_maximumPoolSize = value;

				_AdjustPoolSizeToBounds();
			}
		}

		private void _InitializePool(int minimumPoolSize, int maximumPoolSize, Func<T> factoryMethod)
		{
			_ValidatePoolLimits(minimumPoolSize, maximumPoolSize);

			_factoryMethod = factoryMethod;
			_maximumPoolSize = maximumPoolSize;
			_minimumPoolSize = minimumPoolSize;
			_pooledObjects = new ConcurrentQueue<T>();
			Diagnostics = new ObjectPoolDiagnostics();
			_returnToPoolAction = ReturnObjectToPool;

			_AdjustPoolSizeToBounds();
		}

		private static void _ValidatePoolLimits(int minimumPoolSize, int maximumPoolSize)
		{
			if (minimumPoolSize < 0) throw new ArgumentException("Minimum pool size must be greater or equals to zero.");
			if (maximumPoolSize < 1) throw new ArgumentException("Maximum pool size must be greater than zero.");
			if (minimumPoolSize > maximumPoolSize) throw new ArgumentException("Maximum pool size must be greater than the maximum pool size.");

			Debug.WriteLine("{0}, {1}", minimumPoolSize, maximumPoolSize);
		}

		private void _AdjustPoolSizeToBounds()
		{
			if (Interlocked.CompareExchange(ref _adjustPoolSizeIsInProgressCasFlag, 1, 0) != 0) return;

			while (ObjectsCount < MinimumPoolSize)
			{
				_pooledObjects.Enqueue(_CreatePooledObject());
			}

			while (ObjectsCount > MaximumPoolSize)
			{
				if (!_pooledObjects.TryDequeue(out T dequeuedObjectToDestroy)) continue;

				Diagnostics.IncrementPoolOverflowCount();

				_DestroyPooledObject(dequeuedObjectToDestroy);
			}

			_adjustPoolSizeIsInProgressCasFlag = 0;
		}

		private T _CreatePooledObject()
		{
			var newObject = _factoryMethod == null ? (T) Activator.CreateInstance(typeof(T)) : _factoryMethod();

			Diagnostics.IncrementObjectsCreatedCount();

			newObject.ReturnToPool = _returnToPoolAction;

			return newObject;
		}

		private void _DestroyPooledObject(PooledObject objectToDestroy)
		{
			if (!objectToDestroy.Disposed)
			{
				objectToDestroy.ReleaseResources();
				objectToDestroy.Disposed = true;

				Diagnostics.IncrementObjectsDestroyedCount();
			}

			GC.SuppressFinalize(objectToDestroy);
		}

		/// <summary>
		/// 返回对象池中的一个对象。
		/// </summary>
		/// <returns>返回 <typeparamref name="T"/> 对象的实例。</returns>
		public T GetObject()
		{
			if (_pooledObjects.TryDequeue(out T dequeuedObject))
			{
				ThreadPool.QueueUserWorkItem(o => _AdjustPoolSizeToBounds());

				Diagnostics.IncrementPoolObjectHitCount();

				return dequeuedObject;
			}

			Diagnostics.IncrementPoolObjectMissCount();

			return _CreatePooledObject();
		}

		internal void ReturnObjectToPool(PooledObject objectToReturnToPool, bool reRegisterForFinalization)
		{
			var returnedObject = (T) objectToReturnToPool;

			if (reRegisterForFinalization) Diagnostics.IncrementObjectRessurectionCount();

			if (ObjectsCount < MaximumPoolSize)
			{
				if (!returnedObject.ResetState())
				{
					Diagnostics.IncrementResetStateFailedCount();

					_DestroyPooledObject(returnedObject);

					return;
				}

				if (reRegisterForFinalization) GC.ReRegisterForFinalize(returnedObject);

				Diagnostics.IncrementReturnedToPoolCount();
				_pooledObjects.Enqueue(returnedObject);
			}
			else
			{
				Diagnostics.IncrementPoolOverflowCount();
				_DestroyPooledObject(returnedObject);
			}
		}

		/// <summary>
		/// 析构函数。
		/// </summary>
		~ObjectPool()
		{
			foreach (var item in _pooledObjects) _DestroyPooledObject(item);
		}
	}
}
