// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Net;
using System.Net.Sockets;

namespace Yisoft.Framework
{
    /// <summary>
    /// 提供一种方法，它支持对 <see cref="System.Net.IPAddress"/> 的格式化。
    /// </summary>
    public sealed class IPAddressFormatProvider : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// 使用指定的格式和区域性特定格式设置信息将指定对象的值转换为等效的字符串表示形式。
        /// </summary>
        /// <param name="format">包含格式规范的格式字符串。</param>
        /// <param name="arg">要格式化的对象。</param>
        /// <param name="formatProvider">一个 <see cref="IFormatProvider"/> 对象，它提供有关当前实例的格式信息。</param>
        /// <returns>arg 的值的字符串表示形式，按照 <paramref name="format"/> 和 <paramref name="formatProvider"/> 的指定来进行格式设置。</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null || arg.ToString().Length == 0) return string.Empty;

            if (!(arg is IPAddress ipAddress)) IPAddress.TryParse(arg.ToString(), out ipAddress);

            if (ipAddress == null) return arg.ToString();
            if (string.IsNullOrEmpty(format)) return ipAddress.ToString();

            /* IPV4
             * x	x.x.x.x
             * xx	xx.xx.xx.xx
             * xxx	xxx.xxx.xxx.xxx
             * 
             * IPV6
             * x	x:x:x:x:x:x:x:x
             * xxxx xxxx:xxxx:xxxx:xxxx:xxxx:xxxx:xxxx:xxxx
             * 
             * {0}.{1}.{2}.{3}
             * {0:X2}.{1:X2}.{2:X2}.{3:X2}
             * {0:D3}.{1:D3}.{2:D3}.{3:D3}
             * 
             * {0:X}:{1:X}:{2:X}:{3:X}:{4:X}:{5:X}:{6:X}:{7:X}
             * {0:X4}:{1:X4}:{2:X4}:{3:X4}:{4:X4}:{5:X4}:{6:X4}:{7:X4}
             */

            var iformat = format;
            var b = ipAddress.GetAddressBytes();
            var result = arg.ToString();

            switch (ipAddress.AddressFamily)
            {
                case AddressFamily.InterNetwork:
                    switch (iformat)
                    {
                        case "xxx.xxx.xxx.xxx":
                        case "xxxx":
                        case "xxx":
                            iformat = "{0:D3}.{1:D3}.{2:D3}.{3:D3}";

                            break;
                        case "xx.xx.xx.xx":
                        case "xx":
                            iformat = "{0:X2}.{1:X2}.{2:X2}.{3:X2}";

                            break;
                        case "x.x.x.x":
                        case "x":
                            iformat = "{0}.{1}.{2}.{3}";

                            break;
                    }

                    result = string.Format(iformat, b[0], b[1], b[2], b[3]);

                    break;
                case AddressFamily.InterNetworkV6:
                    switch (iformat)
                    {
                        case "xxxx:xxxx:xxxx:xxxx:xxxx:xxxx:xxxx:xxxx":
                        case "xxxx":
                        case "xxx":
                            iformat =
                                "{0:X2}{1:X2}:{2:X2}{3:X2}:{4:X2}{5:X2}:{6:X2}{7:X2}:{8:X2}{9:X2}:{10:X2}{11:X2}:{12:X2}{13:X2}:{14:X2}{15:X2}";

                            break;
                        case "x:x:x:x:x:x:x:x":
                        case "x":
                            iformat = "{0:X}{1:X}:{2:X}{3:X}:{4:X}{5:X}:{6:X}{7:X}:{8:X}{9:X}:{10:X}{11:X}:{12:X}{13:X}:{14:X}{15:X}";

                            break;
                    }

                    result = string.Format(iformat, b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8], b[9], b[10], b[11], b[12], b[13],
                        b[14], b[15]);

                    break;
            }

            return result;
        }

        /// <summary>
        /// 返回一个对象，该对象为指定类型提供格式设置服务。
        /// </summary>
        /// <param name="formatType">一个对象，该对象指定要返回的格式对象的类型。</param>
        /// <returns>如果 <see cref="IFormatProvider"/> 实现能够提供该类型的对象，则为 formatType 所指定对象的实例；否则为 null。</returns>
        public object GetFormat(Type formatType) { return formatType != null && formatType == typeof(ICustomFormatter) ? this : null; }
    }
}