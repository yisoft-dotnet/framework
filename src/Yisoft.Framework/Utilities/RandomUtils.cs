// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Text;

namespace Yisoft.Framework.Utilities
{
    public class RandomUtils
    {
        /// <summary>
        /// 获取伪随机数生成器的实例。
        /// </summary>
        public static Random Random = new Random();

        /// <summary>
        /// 生成随机字符串。
        /// </summary>
        /// <param name="length">指定要生成的随机字符串的长度。</param>
        /// <param name="seed">指定包含随机字符串种子类型的枚举值。</param>
        /// <param name="custom">指定要生成的随机字符串要包含的自定义字符序列。</param>
        /// <returns>指定长度的随机字符串。</returns>
        public static string GetRandString(int length, RandomSeeds seed = RandomSeeds.Words, string custom = null)
        {
            var result = new StringBuilder();
            var seedBuilder = new StringBuilder();

            if ((seed & RandomSeeds.Number) == RandomSeeds.Number) seedBuilder.Append("0123456789");
            if ((seed & RandomSeeds.Lower) == RandomSeeds.Lower) seedBuilder.Append("abcdefghijklmnopqrstuvwxyz");
            if ((seed & RandomSeeds.Upper) == RandomSeeds.Upper) seedBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            if ((seed & RandomSeeds.Symbol) == RandomSeeds.Symbol) seedBuilder.Append("!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
            if ((seed & RandomSeeds.Custom) == RandomSeeds.Custom) seedBuilder.Append(custom ?? string.Empty);

            for (var i = 0; i < length; i++) result.Append(seedBuilder[Random.Next(0, seedBuilder.Length)]);

            return result.ToString();
        }

        /// <summary>
        /// 生成随机字符串。此方法不会包含默认的特殊字符、大写字母、小写字母和数字的输出，只会包含您指定的字符串序列中的字符。
        /// </summary>
        /// <param name="length">指定要生成的随机字符串的长度。</param>
        /// <param name="custom">指定要生成的随机字符串要包含的自定义字符序列。</param>
        /// <returns>指定长度的随机字符串。</returns>
        public static string GetRandString(int length, string custom) { return GetRandString(length, RandomSeeds.Custom, custom); }

        /// <summary>
        /// 返回一个指定位数的随机数字。
        /// </summary>
        /// <param name="length">要生成的数字的位数。</param>
        /// <returns>指定长度的随机字符串。</returns>
        public static string GenerateInteger(int length)
        {
            if (length < 1) length = 1;

            return GetRandString(length, "0123456789");
        }

        /// <summary>
        /// 返回一个指定位数的随机密码。该密码中剔除了易混淆的字符。
        /// </summary>
        /// <param name="length">要生成的密码的位数。</param>
        /// <param name="numericOnly">指定一个值，该值指示输出结果是否仅包含数字。</param>
        /// <returns>指定长度的随机字符串。</returns>
        public static string GeneratePassword(int length, bool numericOnly = false)
        {
            if (length < 6) length = 6;

            return numericOnly ? GenerateInteger(length) : GetRandString(length, "BCDFGHJKMPQRTVWXY2346789bcdfghjkmpqrtvwxy~!@#$%^&*()");
        }

        public static long GenerateId(int digits)
        {
            if (digits < 1) digits = 1;
            if (digits > 19) digits = 19;

            var value = GetRandString(digits, "0123456789");

            return Convert.ToInt64(value);
        }

        public static long GenerateIdentity(int length = 8, long minValue = 20000000, long maxValue = long.MaxValue)
        {
            if (length < 1) length = 1;
            if (length > 18) length = 18;

            long value;

            while (!long.TryParse(GetRandString(length, "0123456789"), out value) || value < minValue || value > maxValue)
            {
            }

            return value;
        }
    }
}