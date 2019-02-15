// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Threading;

namespace Yisoft.Framework.Threading
{
    /// <summary>
    /// 为对象池中的对象提供基类。
    /// </summary>
    public abstract class PooledObject : IDisposable
    {
        internal Action<PooledObject, bool> ReturnToPool { get; set; }

        internal bool Disposed { get; set; }

        /// <summary>
        /// 在对象销毁时清理资源。
        /// </summary>
        public void Dispose() { ThreadPool.QueueUserWorkItem(o => _HandleReAddingToPool(false)); }

        internal bool ReleaseResources()
        {
            var successFlag = true;

            try
            {
                OnRelease();
            }
            catch (Exception)
            {
                successFlag = false;
            }

            return successFlag;
        }

        internal bool ResetState()
        {
            var successFlag = true;

            try
            {
                OnReset();
            }
            catch (Exception)
            {
                successFlag = false;
            }

            return successFlag;
        }

        /// <summary>
        /// 在重置对象状态时发生。
        /// </summary>
        protected virtual void OnReset() { }

        /// <summary>
        /// 在释放对象时发生。
        /// </summary>
        protected virtual void OnRelease() { }

        private void _HandleReAddingToPool(bool reRegisterForFinalization)
        {
            if (Disposed) return;

            try
            {
                ReturnToPool(this, reRegisterForFinalization);
            }
            catch (Exception)
            {
                Disposed = true;
                ReleaseResources();
            }
        }

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~PooledObject() { _HandleReAddingToPool(true); }
    }
}