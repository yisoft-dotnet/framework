// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Linq;

namespace Yisoft.Framework.Utilities
{
    internal static class MathUtils
    {
        /// <summary>
        /// 表示62进制元字符。
        /// </summary>
        public const string ALPHA_NUMERIC62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 表示36进制元字符。
        /// </summary>
        public const string ALPHA_NUMERIC36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 表示16进制元字符。
        /// </summary>
        public const string ALPHA_NUMERIC16 = "0123456789ABCDEF";

        /// <summary>
        /// 表示52进制元字符。
        /// </summary>
        public const string ALPHA_NUMERIC_ALT52 = "23456789ABCDEFGHJKLMNPRSTUVWXYZabcdefghjkmnpqrstuvwxyz";

        /// <summary>
        /// 表示24进制元字符。
        /// </summary>
        public const string ALPHA_NUMERIC_ALT24 = "2346789BCDFGHJKMPQRTVWXY";

        /// <summary>
        /// 返回 <see cref="long"/> 的指定进制数的字符串表示。
        /// </summary>
        /// <param name="input">指定一个 <see cref="long"/>。</param>
        /// <param name="baseChars">指定目标进制元字符的字符串序列。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        public static string Int64ToBase(long input, string baseChars = ALPHA_NUMERIC62)
        {
            var r = string.Empty;
            var targetBase = baseChars.Length;

            do
            {
                r = $"{baseChars[(int) (input % targetBase)]}{r}";

                input /= targetBase;
            } while (input > 0);

            return r;
        }

        /// <summary>
        /// 返回指定进制数的字符串的 <see cref="long"/> 数字表示。
        /// </summary>
        /// <param name="input">指定一个数字的字符串序列。</param>
        /// <param name="baseChars">指定目标进制元字符的字符串序列。</param>
        /// <returns>返回 <see cref="long"/>。</returns>
        public static long Int64FromBase(string input, string baseChars = ALPHA_NUMERIC62)
        {
            var srcBase = baseChars.Length;
            var r = input.Reverse().ToList();

            return r.Select(t => baseChars.IndexOf(t)).Select((charIndex, i) => charIndex * (long) Math.Pow(srcBase, i)).Sum();
        }

        public static int IntLength(ulong i)
        {
            if (i < 10000000000)
            {
                if (i < 10) return 1;
                if (i < 100) return 2;
                if (i < 1000) return 3;
                if (i < 10000) return 4;
                if (i < 100000) return 5;
                if (i < 1000000) return 6;
                if (i < 10000000) return 7;
                if (i < 100000000) return 8;

                return i < 1000000000 ? 9 : 10;
            }

            if (i < 100000000000) return 11;
            if (i < 1000000000000) return 12;
            if (i < 10000000000000) return 13;
            if (i < 100000000000000) return 14;
            if (i < 1000000000000000) return 15;
            if (i < 10000000000000000) return 16;
            if (i < 100000000000000000) return 17;
            if (i < 1000000000000000000) return 18;

            return i < 10000000000000000000 ? 19 : 20;
        }

        public static char IntToHex(int n) { return n <= 9 ? (char) (n + 48) : (char) (n - 10 + 97); }

        public static int? Min(int? val1, int? val2) { return val1 == null ? val2 : (val2 == null ? val1 : Math.Min(val1.Value, val2.Value)); }

        public static int? Max(int? val1, int? val2) { return val1 == null ? val2 : (val2 == null ? val1 : Math.Max(val1.Value, val2.Value)); }

        public static double? Max(double? val1, double? val2) { return val1 == null ? val2 : (val2 == null ? val1 : Math.Max(val1.Value, val2.Value)); }

        public static bool ApproxEquals(double d1, double d2)
        {
            const double epsilon = 2.2204460492503131E-16;

            if (Math.Abs(d1 - d2) < double.Epsilon) return true;

            var tolerance = (Math.Abs(d1) + Math.Abs(d2) + 10.0) * epsilon;
            var difference = d1 - d2;

            return -tolerance < difference && tolerance > difference;
        }
    }
}