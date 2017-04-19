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
using System.Security.Cryptography;
using System.Text;

namespace Yisoft.Framework.Security.Cryptography
{
	/// <summary>
	/// 提供哈希算法常用操作的封装。
	/// </summary>
	public static class HashAlgorithmHelper
	{
		private static string _ComputeHash(HashAlgorithm hashAlgorithm, string input, bool lowerCase = true, Encoding encoding = null)
		{
			if (encoding == null) encoding = Encoding.UTF8;

			var buffer = hashAlgorithm.ComputeHash(encoding.GetBytes(input));
			var result = BitConverter.ToString(buffer).Replace("-", string.Empty);

			return lowerCase ? result.ToLower() : result;
		}

		/// <summary>
		/// 计算指定字符串的 MD5 哈希值
		/// </summary>
		/// <param name="input">指定一个字符串。</param>
		/// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
		/// <param name="shortValue">指定一个布尔值，该值指示本方法是否应该返回 MD5 哈希值的短码表示形式。</param>
		/// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
		/// <returns>返回指指定字符串的 MD5 哈希值。</returns>
		public static string MD5(string input, bool lowerCase = true, bool shortValue = false, Encoding encoding = null)
		{
			using (var hashAlgorithm = System.Security.Cryptography.MD5.Create())
			{
				var result = _ComputeHash(hashAlgorithm, input, lowerCase, encoding);

				return shortValue ? result.Substring(8, 16) : result;
			}
		}

		/// <summary>
		/// 计算指定字符串的 SHA1 哈希值。
		/// </summary>
		/// <param name="input">指定一个字符串。</param>
		/// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
		/// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
		/// <returns>返回指定字符串的 SHA1 哈希值。</returns>
		public static string SHA1(string input, bool lowerCase = true, Encoding encoding = null)
		{
			using (var hashAlgorithm = System.Security.Cryptography.SHA1.Create())
			{
				return _ComputeHash(hashAlgorithm, input, lowerCase, encoding);
			}
		}

		/// <summary>
		/// 计算指定字符串的 SHA256 哈希值。
		/// </summary>
		/// <param name="input">指定一个字符串。</param>
		/// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
		/// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
		/// <returns>返回指定字符串的 SHA256 哈希值。</returns>
		public static string SHA256(string input, bool lowerCase = true, Encoding encoding = null)
		{
			using (var hashAlgorithm = System.Security.Cryptography.SHA256.Create())
			{
				return _ComputeHash(hashAlgorithm, input, lowerCase, encoding);
			}
		}

		/// <summary>
		/// 计算指定字符串的 SHA384 哈希值。
		/// </summary>
		/// <param name="input">指定一个字符串。</param>
		/// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
		/// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
		/// <returns>返回指定字符串的 SHA384 哈希值。</returns>
		public static string SHA384(string input, bool lowerCase = true, Encoding encoding = null)
		{
			using (var hashAlgorithm = System.Security.Cryptography.SHA384.Create())
			{
				return _ComputeHash(hashAlgorithm, input, lowerCase, encoding);
			}
		}

		/// <summary>
		/// 计算指定字符串的 SHA512 哈希值。
		/// </summary>
		/// <param name="input">指定一个字符串。</param>
		/// <param name="lowerCase">表示输出是否应为小写形式的 <see cref="bool"/>，默认为 false。</param>
		/// <param name="encoding">字符编码，默认使用 <see cref="Encoding.UTF8"/>。</param>
		/// <returns>返回指定字符串的 SHA512 哈希值。</returns>
		public static string SHA512(string input, bool lowerCase = true, Encoding encoding = null)
		{
			using (var hashAlgorithm = System.Security.Cryptography.SHA512.Create())
			{
				return _ComputeHash(hashAlgorithm, input, lowerCase, encoding);
			}
		}
	}
}
