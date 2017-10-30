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
using System.IO;

namespace Yisoft.Framework.IO
{
    /// <summary>
    /// 提供字节序列的进度反馈视图。
    /// </summary>
    public class ProgressStream : Stream
    {
        private readonly int _notifySize;
        private readonly Stream _stream;
        private bool _abort;
        private int _count;

        /// <summary>
        /// 初始化 <see cref="ProgressStream"/> 类的新实例。
        /// </summary>
        /// <param name="stream">一个 <see cref="Stream"/> 对象。</param>
        /// <param name="notifySize">指定一个值，在读取的字节数超出该值时反馈进度。</param>
        public ProgressStream(Stream stream, int notifySize = 8192)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));

            Length = stream.Length;
            _notifySize = notifySize;
        }

        /// <summary>
        /// 获取指示当前流是否支持读取的值。
        /// </summary>
        public override bool CanRead
        {
            get
            {
                lock (this) return _stream.CanRead;
            }
        }

        /// <summary>
        /// 获取指示当前流是否支持查找功能的值。
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                lock (this) return _stream.CanSeek;
            }
        }

        /// <summary>
        /// 获取指示当前流是否支持写入功能的值。
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// 获取用字节表示的流长度。
        /// </summary>
        public override long Length { get; }

        /// <summary>
        /// 获取或设置当前流中的位置。
        /// </summary>
        public override long Position
        {
            get
            {
                lock (this) return _stream.Position;
            }
            set
            {
                lock (this) _stream.Position = value;
            }
        }

        /// <summary>
        /// 在进度改变时发生。
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// 取消读取。
        /// </summary>
        public void AbortRead()
        {
            lock (this) _abort = true;
        }

        /// <summary>
        /// 清除该流的所有缓冲区，并使得所有缓冲数据被写入到基础设备。
        /// </summary>
        public override void Flush()
        {
            lock (this) _stream.Flush();
        }

        /// <summary>
        /// 引发 <see cref="ProgressChangedEventHandler"/> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="ProgressChangedEventArgs"/>。</param>
        protected void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 从当前流读取字节序列，并将此流中的位置提升读取的字节数。
        /// </summary>
        /// <param name="buffer">字节数组。此方法返回时，该缓冲区包含指定的字符数组，该数组的 <paramref name="offset"/> 和 (<paramref name="offset"/> + <paramref name="count"/> -1) 之间的值由从当前源中读取的字节替换。</param>
        /// <param name="offset"><paramref name="buffer"/> 中的从零开始的字节偏移量，从此处开始存储从当前流中读取的数据。</param>
        /// <param name="count">要从当前流中最多读取的字节数。</param>
        /// <returns>读入缓冲区中的总字节数。如果当前可用的字节数没有请求的字节数那么多，则总字节数可能小于请求的字节数；如果已到达流的末尾，则为零 (0)。</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            lock (this)
            {
                if (_abort) throw new IOException("aborted");

                var num = _stream.Read(buffer, offset, count);

                if ((_count += num) < _notifySize && Position != Length) return num;

                OnProgressChanged(new ProgressChangedEventArgs(Position, Length));

                _count = 0;

                return num;
            }
        }

        /// <summary>
        /// 设置当前流中的位置。
        /// </summary>
        /// <param name="offset">相对于 <paramref name="origin"/> 参数的字节偏移量。</param>
        /// <param name="origin"><see cref="SeekOrigin"/> 类型的值，指示用于获取新位置的参考点。</param>
        /// <returns>当前流中的新位置。</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            lock (this) return _stream.Seek(offset, origin);
        }

        /// <summary>
        /// 设置当前流的长度。
        /// </summary>
        /// <param name="value">所需的当前流的长度（以字节表示）。</param>
        public override void SetLength(long value)
        {
            lock (this) _stream.SetLength(value);
        }

        /// <summary>
        /// 向当前流中写入字节序列，并将此流中的当前位置提升写入的字节数。
        /// </summary>
        /// <param name="buffer">字节数组。此方法将 <paramref name="count"/> 个字节从 <paramref name="buffer"/> 复制到当前流。</param>
        /// <param name="offset"><paramref name="buffer"/> 中的从零开始的字节偏移量，从此处开始将字节复制到当前流。</param>
        /// <param name="count">要写入当前流的字节数。</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new IOException("流不可写。");
        }
    }
}
