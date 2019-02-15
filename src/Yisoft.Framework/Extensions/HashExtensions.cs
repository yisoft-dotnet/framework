// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System.Text;
using Yisoft.Framework.Security.Cryptography;

namespace Yisoft.Framework.Extensions
{
    public static class HashExtensions
    {
        /// <summary>
        /// 计算指定字符串的 MD5 哈希值
        /// </summary>
        /// <param name="s">表示文本，即一系列 Unicode 字符。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="System.Boolean"/>，默认为 false。</param>
        /// <param name="shortValue">指定一个布尔值，该值指示本方法是否应该返回 MD5 哈希值的短码表示形式。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回指指定字符串的 MD5 哈希值。</returns>
        public static string MD5(this string s, bool lowerCase = true, bool shortValue = false, Encoding encoding = null)
        {
            return HashAlgorithmHelper.MD5(s, lowerCase, shortValue, encoding);
        }

        /// <summary>
        /// 计算指定字符串的 SHA1 哈希值。
        /// </summary>
        /// <param name="s">指定一个字符串。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="System.Boolean"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回指定字符串的 SHA1 哈希值。</returns>
        public static string SHA1(this string s, bool lowerCase = true, Encoding encoding = null) { return HashAlgorithmHelper.SHA1(s, lowerCase, encoding); }

        /// <summary>
        /// 计算指定字符串的 SHA256 哈希值。
        /// </summary>
        /// <param name="s">指定一个字符串。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="System.Boolean"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回指定字符串的 SHA256 哈希值。</returns>
        public static string SHA256(this string s, bool lowerCase = true, Encoding encoding = null)
        {
            return HashAlgorithmHelper.SHA256(s, lowerCase, encoding);
        }

        /// <summary>
        /// 计算指定字符串的 SHA384 哈希值。
        /// </summary>
        /// <param name="s">指定一个字符串。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="System.Boolean"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回指定字符串的 SHA384 哈希值。</returns>
        public static string SHA384(this string s, bool lowerCase = true, Encoding encoding = null)
        {
            return HashAlgorithmHelper.SHA384(s, lowerCase, encoding);
        }

        /// <summary>
        /// 计算指定字符串的 SHA512 哈希值。
        /// </summary>
        /// <param name="s">指定一个字符串。</param>
        /// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="System.Boolean"/>，默认为 false。</param>
        /// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
        /// <returns>返回指定字符串的 SHA512 哈希值。</returns>
        public static string SHA512(this string s, bool lowerCase = true, Encoding encoding = null)
        {
            return HashAlgorithmHelper.SHA512(s, lowerCase, encoding);
        }
    }
}