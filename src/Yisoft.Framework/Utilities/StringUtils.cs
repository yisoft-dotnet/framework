// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Yisoft.Framework.Utilities
{
    public static class StringUtils
    {
        /// <summary>
        /// 表示正则表达式选项。
        /// </summary>
        private const RegexOptions _REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.Singleline;

        public static readonly string[] EmptyStringArray = { };

        /// <summary>
        /// 返回已格式化的包含HTML标记的文本内容。
        /// </summary>
        /// <param name="input">要为其添加HTML标记的字符串。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        public static string ApplyHtml(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            var output = input.Trim();

            output = Regex.Replace(output, @"(\r\n)+", "</p><p>");
            output = Regex.Replace(output, @"[\r\n]+", "<br />");
            output = Regex.Replace(output, @" +", " ");
            output = Regex.Replace(output, @"[\t　]+", string.Empty).Trim();

            return output.Length == 0 ? string.Empty : string.Concat("<p>", output, "</p>");
        }

        /// <summary>
        /// 返回清除了空行的字符串的副本。
        /// </summary>
        /// <param name="text">要处理的字符串。</param>
        /// <returns><see cref="string"/>。</returns>
        public static string StripBlankLine(string text) { return string.IsNullOrEmpty(text) ? string.Empty : Regex.Replace(text, @"\n\s*\n", "\n", _REGEX_OPTIONS); }

        /// <summary>
        /// 返回清除了所有 HTML 标记的字符串的副本。
        /// </summary>
        /// <param name="text">包含要处理的文本的字符串。</param>
        /// <returns>str<see cref="string"/>。ing</returns>
        public static string StripHtml(string text) { return string.IsNullOrEmpty(text) ? string.Empty : Regex.Replace(text, @"<\/?[^>]*>", string.Empty, _REGEX_OPTIONS); }

        /// <summary>
        /// 在指定的输入字符串内，使用指定的替换字符串替换与指定正则表达式匹配的所有字符串。指定的选项将修改匹配操作。
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串。</param>
        /// <param name="pattern">要匹配的正则表达式模式。</param>
        /// <param name="replacement">替换字符串。</param>
        /// <returns>一个与输入字符串基本相同的新字符串，唯一的差别在于，其中的每个匹配字符串已被替换字符串代替。</returns>
        public static string RegexReplace(string input, string pattern, string replacement)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            if (string.IsNullOrEmpty(pattern)) return input;

            if (string.IsNullOrEmpty(replacement)) replacement = string.Empty;

            return Regex.Replace(input, pattern, replacement, _REGEX_OPTIONS);
        }

        /// <summary>
        /// 以指定的分隔符分割字符串。
        /// </summary>
        /// <param name="input">要拆分的字符串。</param>
        /// <param name="pattern">要匹配的正则表达式模式。</param>
        /// <returns>字符串数组。</returns>
        public static string[] RegexSplit(string input, string pattern)
        {
            if (string.IsNullOrEmpty(input)) return new string[] { };
            if (string.IsNullOrEmpty(pattern)) return new string[] { };

            return input.IndexOf(pattern, StringComparison.Ordinal) < 0
                ? new[] {input}
                : Regex.Split(input, pattern.Replace(".", @"\."), _REGEX_OPTIONS);
        }

        /// <summary>
        /// 按字节截取字符串。
        /// </summary>
        /// <param name="content">包含要截取的内容的字符串。</param>
        /// <param name="startindex">要截取的子字符串的开始位置。</param>
        /// <param name="length">要截取的子字符串的长度。</param>
        /// <param name="append">如果被截取，可以指定尾部要追加的内容。</param>
        /// <returns><see cref="string"/>。</returns>
        public static string SubString(string content, int startindex, int length, string append)
        {
            if (string.IsNullOrEmpty(content)) return string.Empty;
            if (length <= 0) return string.Empty;

            if (startindex < 0) startindex = 0;
            if (string.IsNullOrEmpty(append)) append = string.Empty;

            var u8 = Encoding.UTF8;
            var su8 = content.ToCharArray();
            var blen = u8.GetByteCount(su8);

            if (blen < startindex) return content;

            if (length <= 0) length = blen;
            if (length <= 1) length = 2;

            var str = string.Empty;
            var endindex = startindex + length;
            var n = 0;
            var l = su8.Length;

            if (blen == endindex) return content;

            for (var i = 0; i < l; i++)
            {
                if (n >= blen) break;

                var c = u8.GetByteCount(su8[i].ToString());

                n = c > 1 ? n + 2 : n + 1;

                if (n > startindex && n <= endindex) str += su8[i];
            }

            return str.Length == content.Length ? str.Trim() : str.Trim() + append.Trim();
        }

        /// <summary>
        /// 获取指定字符串的字节长度。
        /// </summary>
        /// <param name="content">包含要获取其长度的内容的字符串。</param>
        /// <param name="encoding">指定字符编码。</param>
        /// <returns>int</returns>
        public static int GetBytesLength(string content, string encoding)
        {
            if (string.IsNullOrEmpty(content)) return 0;

            if (string.IsNullOrEmpty(encoding)) encoding = "UTF-8";

            return Encoding.GetEncoding(encoding).GetByteCount(content);
        }

        /// <summary>
        /// 将该字符串文本转换为 <see cref="bool"/> 类型。
        /// </summary>
        /// <param name="str">表示文本，即一系列 Unicode 字符。</param>
        /// <returns><see cref="bool"/></returns>
        public static bool ToBoolean(string str)
        {
            return !string.IsNullOrEmpty(str)
                   &&
                   (string.Compare(str, "true", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(str, "yes", StringComparison.OrdinalIgnoreCase) == 0 ||
                    str == "1");
        }

        /// <summary>
        /// 格式化文件尺寸。
        /// </summary>
        /// <param name="size">指定包含文件大小的字节数。</param>
        /// <param name="format">使用指定的格式，将此实例的数值转换为它的等效字符串表示形式。</param>
        /// <returns>返回表示文件大小的带有单位的字符串。</returns>
        public static string FileSizeFormat(long size, string format = "F2")
        {
            if (string.IsNullOrWhiteSpace(format)) throw new ArgumentOutOfRangeException(nameof(format));

            var u = new[] {"B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB", "NB", "DB"};
            var n = 0;
            double i = size;

            while (i > 1024)
            {
                i = i / 1024.0;

                if (n++ == 10) break;
            }

            return i.ToString(format) + u[n];
        }

        /// <summary>
        /// 将该字符串中指定位置的字符替换为指定的遮蔽字符。
        /// </summary>
        /// <param name="str">表示文本，即一系列 Unicode 字符。</param>
        /// <param name="index">要遮蔽的字符的开始位置。</param>
        /// <param name="length">要遮蔽的字符串长度。</param>
        /// <param name="maskChar">指定遮蔽字符。</param>
        /// <returns><see cref="string"/>。</returns>
        public static string Mask(string str, int index, int length, char maskChar = '*')
        {
            if (str == null) return null;

            var strLength = str.Length;

            if (index < 0 || index > strLength - 1 || length <= 0) return str;

            var minIndex = index;
            var maxIndex = index + length;

            if (maxIndex >= strLength)
            {
                maxIndex = strLength;

                if (minIndex == 0) return string.Empty.PadLeft(strLength, maskChar);
            }

            maxIndex--;

            var s = new StringBuilder();

            for (var i = 0; i < strLength; i++)
            {
                if (i < minIndex || i > maxIndex) s.Append(str[i]);
                else s.Append(maskChar);
            }

            return s.ToString();
        }

        public static void ToCharAsUnicode(char c, char[] buffer)
        {
            buffer[0] = '\\';
            buffer[1] = 'u';
            buffer[2] = MathUtils.IntToHex((c >> 12) & '\x000f');
            buffer[3] = MathUtils.IntToHex((c >> 8) & '\x000f');
            buffer[4] = MathUtils.IntToHex((c >> 4) & '\x000f');
            buffer[5] = MathUtils.IntToHex(c & '\x000f');
        }

        public static string ToUnderscoreUpperCase(string original, bool camelCase = true)
        {
            var words = SplitByCharacterType(original, camelCase);

            return string.Join("_", words).ToUpper();
        }

        public static string ToUnderscoreLowerCase(string original, bool camelCase = true)
        {
            var words = SplitByCharacterType(original, camelCase);

            return string.Join("_", words).ToLower();
        }

        public static string ToHyphenUpperCase(string original, bool camelCase = true)
        {
            var words = SplitByCharacterType(original, camelCase);

            return string.Join("-", words).ToUpper();
        }

        public static string ToHyphenLowerCase(string original, bool camelCase = true)
        {
            var words = SplitByCharacterType(original, camelCase);

            return string.Join("-", words).ToLower();
        }

        public static string ToDotUpperCase(string original, bool camelCase = true)
        {
            var words = SplitByCharacterType(original, camelCase);

            return string.Join(".", words).ToUpper();
        }

        public static string ToDotLowerCase(string original, bool camelCase = true)
        {
            var words = SplitByCharacterType(original, camelCase);

            return string.Join(".", words).ToLower();
        }

        public static string ToWordUpperCase(string original, bool camelCase = true)
        {
            var words = SplitByCharacterType(original, camelCase);

            return string.Join(" ", words).ToUpper();
        }

        public static string ToWordLowerCase(string original, bool camelCase = true)
        {
            var words = SplitByCharacterType(original, camelCase);

            return string.Join(" ", words).ToLower();
        }

        public static string ToAllUpperCase(string original, bool camelCase = true) { return string.Concat(SplitByCharacterType(original, camelCase)).ToUpper(); }

        public static string ToAllLowerCase(string original, bool camelCase = true) { return string.Concat(SplitByCharacterType(original, camelCase)).ToLower(); }

        public static string ToCamelCase(string original)
        {
            var s = ToPascalCase(original);

            return s.Length < 1 ? string.Empty : char.ToLower(s[0]) + s.Substring(1);
        }

        public static string ToPascalCase(string original)
        {
            var words = SplitByCharacterType(original);
            var s = new StringBuilder();

            foreach (var word in words)
            {
                s.Append(char.ToUpper(word[0]));
                s.Append(word.Substring(1).ToLower());
            }

            return s.ToString();
        }

        public static string[] SplitByCharacterType(string original, bool camelCase = true,
            StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
        {
            if (string.IsNullOrWhiteSpace(original)) return EmptyStringArray;

            var charArray = original.Trim().ToCharArray();
            var list = new List<string>();
            var tokenStart = 0;
            var currentType = CharUnicodeInfo.GetUnicodeCategory(charArray[tokenStart]);

            for (var pos = tokenStart + 1; pos < charArray.Length; pos++)
            {
                var type = CharUnicodeInfo.GetUnicodeCategory(charArray[pos]);

                if (type == currentType) continue;

                string value = null;

                if (type == UnicodeCategory.ModifierSymbol
                    || type == UnicodeCategory.ConnectorPunctuation
                    || type == UnicodeCategory.DashPunctuation
                    || type == UnicodeCategory.OtherPunctuation)
                {
                    var newTokenStart = pos - 0;

                    if (newTokenStart != tokenStart)
                    {
                        value = new string(charArray, tokenStart, newTokenStart - tokenStart);

                        tokenStart = newTokenStart + 1;
                    }
                }
                else if (camelCase && type == UnicodeCategory.LowercaseLetter && currentType == UnicodeCategory.UppercaseLetter)
                {
                    var newTokenStart = pos - 1;

                    if (newTokenStart != tokenStart)
                    {
                        value = new string(charArray, tokenStart, newTokenStart - tokenStart);

                        tokenStart = newTokenStart;
                    }
                }
                else
                {
                    value = new string(charArray, tokenStart, pos - tokenStart);

                    tokenStart = pos;
                }

                //Console.WriteLine(@"{0} -- {1} -- {2} {3} -- {4}", charArray[pos], type, tokenStart, pos, value);

                currentType = type;

                if (value == null) continue;
                if (splitOptions == StringSplitOptions.RemoveEmptyEntries && string.IsNullOrWhiteSpace(value)) continue;

                list.Add(value);
            }

            list.Add(new string(charArray, tokenStart, charArray.Length - tokenStart));

            return list.ToArray();
        }

        public static string ToSBC(string input)
        {
            var c = input.ToCharArray();

            for (var i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char) 12288;

                    continue;
                }

                if (c[i] < 127) c[i] = (char) (c[i] + 65248);
            }

            return new string(c);
        }

        public static string ToDBC(string input)
        {
            var c = input.ToCharArray();

            for (var i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char) 32;

                    continue;
                }

                if (c[i] > 65280 && c[i] < 65375) c[i] = (char) (c[i] - 65248);
            }

            return new string(c);
        }
    }
}