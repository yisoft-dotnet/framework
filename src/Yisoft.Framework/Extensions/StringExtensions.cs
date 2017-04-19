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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Yisoft.Framework.Utilities;

namespace Yisoft.Framework.Extensions
{
	/// <summary>
	/// 为 <see cref="string"/> 对象提供扩展方法。
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// 表示电子信箱地址的正则表达式模式的字符串。
		/// </summary>
		public const string EMAIL_PATTERN = @"^([a-z0-9][a-z0-9_\-\.\+]*)@(([a-z0-9][a-z0-9\.\-]{0,63})\.([a-z]{1,5}))$";

		/// <summary>
		/// 表示 IP 地址的正则表达式模式的字符串。
		/// </summary>
		public const string IP_ADDRESS_PATTERN =
				@"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])"
			;

		/// <summary>
		/// 表示 Uri 地址的正则表达式模式的字符串。
		/// </summary>
		public const string URI_PATTERN =
				@"^((https|http|ftp|rtsp|mms)?://)?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?(([0-9]{1,3}\.){3}[0-9]{1,3}|([0-9a-z_!~*'()-]+\.)*([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\.[a-z]{2,6})(:[0-9]{1,4})?((/?)|(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$"
			;

		/// <summary>
		/// 验证给定的字符串是否与指定的正则表达式相匹配。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <param name="pattern">指定一个要匹配的正则表达式模式。</param>
		/// <param name="options">正则表达式选项。</param>
		/// <returns>如果给定的字符串与指定的正则表达式模式相匹配则返回 true，否则返回 false。</returns>
		public static bool IsMatch(this string s, string pattern, RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline)
		{
			if (string.IsNullOrWhiteSpace(s) || string.IsNullOrWhiteSpace(pattern)) return false;

			return Regex.IsMatch(s, pattern, options);
		}

		/// <summary>
		/// 验证给定的字符串是否符合电子信箱地址的规则。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <returns>如果给定的字符串符合电子信箱地址的规则则返回 true，否则返回 false。</returns>
		public static bool IsEmail(this string s)
		{
			return s.IsMatch(EMAIL_PATTERN);
		}

		/// <summary>
		/// 验证给定的字符串是否符合中国大陆手机号码规则。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <returns>如果给定的字符串符合中国大陆手机号码规则则返回 true，否则返回 false。</returns>
		public static bool IsChinaMobilePhoneNumber(this string s)
		{
			return s.Length == 11 && s[0] == '1' && s.IsNumberOnly();
		}

		/// <summary>
		/// 验证给定的字符串是否符合中国大陆邮政编码规则。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <returns>如果给定的字符串符合中国大陆邮政编码规则则返回 true，否则返回 false。</returns>
		public static bool IsChinaPostalCode(this string s)
		{
			return (s.Length == 5 || s.Length == 6) && s.IsNumberOnly();
		}

		/// <summary>
		/// 验证给定的字符串是否符合中国大陆电话区号规则。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <returns>如果给定的字符串符合中国大陆电话区号规则则返回 true，否则返回 false。</returns>
		public static bool IsChinaTelephoneAreaCode(this string s)
		{
			return (s.Length == 3 || s.Length == 4) && s[0] == '0' && s.IsNumberOnly();
		}

		/// <summary>
		/// 验证给定的字符串是否能够转换为数字。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <returns>如果给定的字符串能够转换为数字则返回 true，否则返回 false。</returns>
		public static bool IsNumeric(this string s)
		{
			return s.All(char.IsNumber);
		}

		/// <summary>
		/// 验证给定的字符串是否仅包含数字。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <returns>如果给定的字符串仅包含数字则返回 true，否则返回 false。</returns>
		public static bool IsNumberOnly(this string s)
		{
			return !string.IsNullOrEmpty(s) && s.All(char.IsDigit);
		}

		/// <summary>
		/// 验证给定的字符串是否是合法的 IP 地址格式（注意此方法不验证 IP 地址的有效性）。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <returns>如果给定的字符串符合 IP 地址的规则则返回 true，否则返回false。</returns>
		public static bool IsIPAddress(this string s)
		{
			return s.IsMatch(IP_ADDRESS_PATTERN);
		}

		/// <summary>
		/// 验证给定的字符串是否是合法的 Uri 地址。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <returns>如果给定的字符串符合 Uri 规则则返回 true，否则返回false。</returns>
		public static bool IsUri(this string s)
		{
			return s.IsMatch(URI_PATTERN);
		}

		/// <summary>
		/// 将该字符串文本转换为 Boolean 类型。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <returns><see cref="bool"/></returns>
		public static bool ToBoolean(this string s)
		{
			return StringUtils.ToBoolean(s);
		}

		/// <summary>
		/// 将该字符串中指定位置的字符替换为指定的遮蔽字符。
		/// </summary>
		/// <param name="s">表示文本，即一系列 Unicode 字符。</param>
		/// <param name="index">要遮蔽的字符的开始位置。</param>
		/// <param name="length">要遮蔽的字符串长度。</param>
		/// <param name="maskChar">指定遮蔽字符。</param>
		/// <returns><see cref="string"/>。</returns>
		public static string Mask(this string s, int index, int length, char maskChar = '*')
		{
			return StringUtils.Mask(s, index, length, maskChar);
		}

		public static string ToUnderscoreUpperCase(this string s) { return StringUtils.ToUnderscoreUpperCase(s); }

		public static string ToUnderscoreLowerCase(this string s) { return StringUtils.ToUnderscoreLowerCase(s); }

		public static string ToHyphenUpperCase(this string s) { return StringUtils.ToHyphenUpperCase(s); }

		public static string ToHyphenLowerCase(this string s) { return StringUtils.ToHyphenLowerCase(s); }

		public static string ToDotUpperCase(this string s) { return StringUtils.ToDotUpperCase(s); }

		public static string ToDotLowerCase(this string s) { return StringUtils.ToDotLowerCase(s); }

		public static string ToWordUpperCase(this string s) { return StringUtils.ToWordUpperCase(s); }

		public static string ToWordLowerCase(this string s) { return StringUtils.ToWordLowerCase(s); }

		public static string ToAllUpperCase(this string s) { return StringUtils.ToAllUpperCase(s); }

		public static string ToAllLowerCase(this string s) { return StringUtils.ToAllLowerCase(s); }

		public static string ToCamelCase(this string s) { return StringUtils.ToCamelCase(s); }

		public static string ToPascalCase(this string s) { return StringUtils.ToPascalCase(s); }

		[DebuggerStepThrough]
		public static string ToSpaceSeparatedString(this IEnumerable<string> list)
		{
			if (list == null) return string.Empty;

			var sb = new StringBuilder(100);

			foreach (var element in list)
			{
				sb.Append(element + " ");
			}

			return sb.ToString().Trim();
		}

		[DebuggerStepThrough]
		public static IEnumerable<string> FromSpaceSeparatedString(this string input)
		{
			input = input.Trim();

			return input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
		}

		public static List<string> ParseScopesString(this string scopes)
		{
			if (scopes.IsMissing()) return null;

			scopes = scopes.Trim();

			var parsedScopes = scopes.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();

			if (!parsedScopes.Any()) return null;

			parsedScopes.Sort();

			return parsedScopes;
		}

		[DebuggerStepThrough]
		public static bool IsMissing(this string value) => string.IsNullOrWhiteSpace(value);

		[DebuggerStepThrough]
		public static bool IsMissingOrTooLong(this string value, int maxLength) => string.IsNullOrWhiteSpace(value) || value.Length > maxLength;

		[DebuggerStepThrough]
		public static bool IsPresent(this string value) => !string.IsNullOrWhiteSpace(value);

		[DebuggerStepThrough]
		public static string EnsureLeadingSlash(this string url) => !url.StartsWith("/") ? "/" + url : url;

		[DebuggerStepThrough]
		public static string EnsureTrailingSlash(this string url) => !url.EndsWith("/") ? url + "/" : url;

		[DebuggerStepThrough]
		public static string RemoveLeadingSlash(this string url)
		{
			if (url != null && url.StartsWith("/")) url = url.Substring(1);

			return url;
		}

		[DebuggerStepThrough]
		public static string RemoveTrailingSlash(this string url)
		{
			if (url != null && url.EndsWith("/")) url = url.Substring(0, url.Length - 1);

			return url;
		}

		[DebuggerStepThrough]
		public static string CleanUrlPath(this string url)
		{
			if (string.IsNullOrWhiteSpace(url)) url = "/";

			if (url != "/" && url.EndsWith("/")) url = url.Substring(0, url.Length - 1);

			return url;
		}

		[DebuggerStepThrough]
		public static bool IsLocalUrl(this string url)
		{
			return
				!string.IsNullOrEmpty(url) &&

				// Allows "/" or "/foo" but not "//" or "/\".
				(url[0] == '/' && (url.Length == 1 || url[1] != '/' && url[1] != '\\') ||

				 // Allows "~/" or "~/foo".
				 url.Length > 1 && url[0] == '~' && url[1] == '/');
		}

		[DebuggerStepThrough]
		public static string AddQueryString(this string url, string query)
		{
			if (!url.Contains("?")) url += "?";
			else if (!url.EndsWith("&")) url += "&";

			return url + query;
		}

		[DebuggerStepThrough]
		public static string AddHashFragment(this string url, string query)
		{
			if (!url.Contains("#")) url += "#";

			return url + query;
		}

		public static string GetOrigin(this string url)
		{
			if (url == null || !url.StartsWith("http://") && !url.StartsWith("https://")) return null;

			var idx = url.IndexOf("//", StringComparison.Ordinal);

			if (idx <= 0) return null;

			idx = url.IndexOf("/", idx + 2, StringComparison.Ordinal);

			if (idx >= 0) url = url.Substring(0, idx);

			return url;
		}
	}
}
