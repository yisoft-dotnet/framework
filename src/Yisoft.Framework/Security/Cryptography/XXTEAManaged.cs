// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace Yisoft.Framework.Security.Cryptography
{
    /// <summary>
    /// 使用 XXTEA 算法将输入数据加密或解密。
    /// </summary>
    public sealed class XXTEAManaged
    {
        private static uint[] _Encrypt(uint[] v, uint[] k)
        {
            var n = v.Length - 1;

            if (n < 1) return v;

            if (k.Length < 4)
            {
                var key1 = new uint[4];

                k.CopyTo(key1, 0);

                k = key1;
            }

            var z = v[n];
            const uint delta = 0x9E3779B9;
            uint sum = 0;
            var q = 6 + 52 / (n + 1);

            while (q-- > 0)
            {
                sum = unchecked(sum + delta);
                var e = (sum >> 2) & 3;

                uint y;
                int p;

                for (p = 0; p < n; p++)
                {
                    y = v[p + 1];
                    z = unchecked(v[p] += (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(p & 3) ^ e] ^ z)));
                }

                y = v[0];
                z = unchecked(v[n] += (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(p & 3) ^ e] ^ z)));
            }

            return v;
        }

        private static uint[] _Decrypt(uint[] v, uint[] k)
        {
            var n = v.Length - 1;

            if (n < 1) return v;

            if (k.Length < 4)
            {
                var key1 = new uint[4];

                k.CopyTo(key1, 0);

                k = key1;
            }

            var y = v[0];
            const uint delta = 0x9E3779B9;
            var q = 6 + 52 / (n + 1);
            var sum = unchecked((uint)(q * delta));

            while (sum != 0)
            {
                var e = (sum >> 2) & 3;

                int p;
                uint z;

                for (p = n; p > 0; p--)
                {
                    z = v[p - 1];
                    y = unchecked(v[p] -= (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(p & 3) ^ e] ^ z)));
                }

                z = v[n];
                y = unchecked(v[0] -= (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(p & 3) ^ e] ^ z)));
                sum = unchecked(sum - delta);
            }

            return v;
        }

        private static uint[] _ToUInt32Array(IList<byte> data, bool includeLength)
        {
            var n = (data.Count & 3) == 0 ? data.Count >> 2 : (data.Count >> 2) + 1;
            uint[] result;

            if (includeLength)
            {
                result = new uint[n + 1];
                result[n] = (uint)data.Count;
            }
            else result = new uint[n];

            n = data.Count;

            for (var i = 0; i < n; i++) result[i >> 2] |= (uint)data[i] << ((i & 3) << 3);

            return result;
        }

        private static byte[] _ToByteArray(IList<uint> data, bool includeLength)
        {
            int n;

            if (includeLength) n = (int)data[^1];
            else n = data.Count << 2;

            var result = new byte[n];

            for (var i = 0; i < n; i++) result[i] = (byte)(data[i >> 2] >> ((i & 3) << 3));

            return result;
        }

        /// <summary>
        /// 加密字符串。
        /// </summary>
        /// <param name="str">要加密的字符串。</param>
        /// <param name="key">安全密钥。</param>
        /// <returns>返回 <see cref="string"/> 。</returns>
        public static string Encrypt(string str, string key)
        {
            var e = Encoding.UTF8;
            var b = Encrypt(e.GetBytes(str), e.GetBytes(key));

            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// 解密字符串。
        /// </summary>
        /// <param name="str">要解密的字符串。</param>
        /// <param name="key">安全密钥。</param>
        /// <returns>返回 <see cref="string"/> 。</returns>
        public static string Decrypt(string str, string key)
        {
            if (string.IsNullOrWhiteSpace(str)) return string.Empty;

            var encoding = Encoding.UTF8;

            return encoding.GetString(Decrypt(Convert.FromBase64String(str), encoding.GetBytes(key)));
        }

        /// <summary>
        /// 加密 <see cref="byte"/> 数组。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">安全密钥。</param>
        /// <returns>返回 <see cref="byte"/> 数组。</returns>
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (key == null) throw new ArgumentNullException(nameof(key));

            return data.Length == 0
                ? data
                : _ToByteArray(_Encrypt(_ToUInt32Array(data, true), _ToUInt32Array(key, false)), false);
        }

        /// <summary>
        /// 解密 <see cref="byte"/> 数组。
        /// </summary>
        /// <param name="data">要解密的字节数组。</param>
        /// <param name="key">安全密钥。</param>
        /// <returns>返回 <see cref="byte"/> 数组。</returns>
        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (key == null) throw new ArgumentNullException(nameof(key));

            return data.Length == 0
                ? data
                : _ToByteArray(_Decrypt(_ToUInt32Array(data, false), _ToUInt32Array(key, false)), true);
        }
    }
}