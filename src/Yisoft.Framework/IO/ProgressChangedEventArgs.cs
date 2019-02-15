// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;

namespace Yisoft.Framework.IO
{
    /// <summary>
    /// 为 <see cref="ProgressStream.ProgressChanged"/> 事件提供数据。
    /// </summary>
    public class ProgressChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化 <see cref="ProgressChangedEventArgs"/> 类的新实例。
        /// </summary>
        /// <param name="position">当前流中的位置。</param>
        /// <param name="length">当前流的总长度。</param>
        public ProgressChangedEventArgs(long position, long length)
        {
            Position = position;
            Length = length;
            ProgressPercentage = Convert.ToInt32(position * 100.0 / length);
        }

        /// <summary>
        /// 获取进度百分比。
        /// </summary>
        public int ProgressPercentage { get; }

        /// <summary>
        /// 获取当前流中的位置。
        /// </summary>
        public long Position { get; }

        /// <summary>
        /// 获取当前流的总长度。
        /// </summary>
        public long Length { get; }
    }
}