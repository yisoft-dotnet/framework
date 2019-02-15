// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Yisoft.Framework.Security.Cryptography
{
    /// <summary>
    /// 提供对基于哈希的消息验证代码 (HMAC) 的常用操作的封装。
    /// </summary>
    public static class HMACHelper
    {
        /// <summary>
        /// 计算指定字节数组的哈希值。
        /// </summary>
        /// <typeparam name="T">指定一个 <see cref="HMAC"/> 算法的实现。</typeparam>
        /// <param name="input">要计算其哈希代码的输入。</param>
        /// <param name="key">加密的机密密钥。密钥的长度不限，但如果超过 64 个字节，就会对其进行哈希计算（使用 SHA-1），以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>计算所得的哈希代码。</returns>
        public static byte[] ComputeHash<T>(Stream input, byte[] key) where T : HMAC, new()
        {
            using (var s = new T())
            {
                s.Key = key;

                return s.ComputeHash(input);
            }
        }

        /// <summary>
        /// 计算指定字节数组的哈希值。
        /// </summary>
        /// <typeparam name="T">指定一个 <see cref="HMAC"/> 算法的实现。</typeparam>
        /// <param name="input">要计算其哈希代码的输入。</param>
        /// <param name="key">加密的机密密钥。密钥的长度不限，但如果超过 64 个字节，就会对其进行哈希计算（使用 SHA-1），以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>计算所得的哈希代码。</returns>
        public static byte[] ComputeHash<T>(byte[] input, byte[] key) where T : HMAC, new()
        {
            using (var s = new T())
            {
                s.Key = key;

                return s.ComputeHash(input);
            }
        }

        /// <summary>
        /// 计算指定字节数组的哈希值。
        /// </summary>
        /// <typeparam name="T">指定一个 <see cref="HMAC"/> 算法的实现。</typeparam>
        /// <param name="input">要计算其哈希代码的输入。</param>
        /// <param name="key">加密的机密密钥。密钥的长度不限，但如果超过 64 个字节，就会对其进行哈希计算（使用 SHA-1），以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回表示计算所得的哈希代码的字符串。</returns>
        public static string ComputeHash<T>(string input, string key, bool lowerCase = true, Encoding encoding = null) where T : HMAC, new()
        {
            if (encoding == null) encoding = Encoding.UTF8;

            var hashValue = BitConverter.ToString(ComputeHash<T>(encoding.GetBytes(input), encoding.GetBytes(key)));

            return lowerCase ? hashValue.ToLower() : hashValue;
        }

        /// <summary>
        /// 使用 <see cref="HMACMD5"/> 计算指定字节数组的哈希值。
        /// </summary>
        /// <param name="input">要计算其哈希代码的输入。</param>
        /// <param name="key">加密的机密密钥。密钥的长度不限，但如果超过 64 个字节，就会对其进行哈希计算（使用 SHA-1），以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回表示计算所得的哈希代码的字符串。</returns>
        public static string HMACMD5(string input, string key, bool lowerCase = true, Encoding encoding = null)
        {
            return ComputeHash<HMACMD5>(input, key, lowerCase, encoding);
        }

        /// <summary>
        /// 使用 <see cref="HMACSHA1"/> 计算指定字节数组的哈希值。
        /// </summary>
        /// <param name="input">要计算其哈希代码的输入。</param>
        /// <param name="key">加密的机密密钥。密钥的长度不限，但如果超过 64 个字节，就会对其进行哈希计算（使用 SHA-1），以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回表示计算所得的哈希代码的字符串。</returns>
        public static string HMACSHA1(string input, string key, bool lowerCase = true, Encoding encoding = null)
        {
            return ComputeHash<HMACSHA1>(input, key, lowerCase, encoding);
        }

        /// <summary>
        /// 使用 <see cref="HMACSHA256"/> 计算指定字节数组的哈希值。
        /// </summary>
        /// <param name="input">要计算其哈希代码的输入。</param>
        /// <param name="key">加密的机密密钥。密钥的长度不限，但如果超过 64 个字节，就会对其进行哈希计算（使用 SHA-1），以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回表示计算所得的哈希代码的字符串。</returns>
        public static string HMACSHA256(string input, string key, bool lowerCase = true, Encoding encoding = null)
        {
            return ComputeHash<HMACSHA256>(input, key, lowerCase, encoding);
        }

        /// <summary>
        /// 使用 <see cref="HMACSHA384"/> 计算指定字节数组的哈希值。
        /// </summary>
        /// <param name="input">要计算其哈希代码的输入。</param>
        /// <param name="key">加密的机密密钥。密钥的长度不限，但如果超过 64 个字节，就会对其进行哈希计算（使用 SHA-1），以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回表示计算所得的哈希代码的字符串。</returns>
        public static string HMACSHA384(string input, string key, bool lowerCase = true, Encoding encoding = null)
        {
            return ComputeHash<HMACSHA384>(input, key, lowerCase, encoding);
        }

        /// <summary>
        /// 使用 <see cref="HMACSHA512"/> 计算指定字节数组的哈希值。
        /// </summary>
        /// <param name="input">要计算其哈希代码的输入。</param>
        /// <param name="key">加密的机密密钥。密钥的长度不限，但如果超过 64 个字节，就会对其进行哈希计算（使用 SHA-1），以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回表示计算所得的哈希代码的字符串。</returns>
        public static string HMACSHA512(string input, string key, bool lowerCase = true, Encoding encoding = null)
        {
            return ComputeHash<HMACSHA512>(input, key, lowerCase, encoding);
        }
    }
}