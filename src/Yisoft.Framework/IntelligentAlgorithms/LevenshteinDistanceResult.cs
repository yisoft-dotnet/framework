// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Text;

namespace Yisoft.Framework.IntelligentAlgorithms
{
    /// <summary>
    /// 表示 <see cref="LevenshteinDistance"/> 的运算结果。
    /// </summary>
    public struct LevenshteinDistanceResult : IEquatable<LevenshteinDistanceResult>, IComparable<LevenshteinDistanceResult>
    {
        /// <summary>
        /// 初始化 <see cref="LevenshteinDistanceResult"/> 对象。
        /// </summary>
        /// <param name="str1">参与比较的第一个字符串。</param>
        /// <param name="str2">参与比较的第二个字符串。</param>
        /// <param name="computeTimes">运算次数。</param>
        /// <param name="distance">编辑距离。</param>
        /// <param name="rate">相似度。</param>
        public LevenshteinDistanceResult(string str1, string str2, int computeTimes, int distance, double rate)
            : this()
        {
            String1 = str1 ?? throw new ArgumentNullException(nameof(str1));
            String2 = str2 ?? throw new ArgumentNullException(nameof(str2));

            if (computeTimes < 0) throw new ArgumentOutOfRangeException(nameof(computeTimes));
            if (distance < 0) throw new ArgumentOutOfRangeException(nameof(distance));
            if (rate < 0 || rate > 1) throw new ArgumentOutOfRangeException(nameof(rate));

            ComputeTimes = computeTimes;
            Distance = distance;
            Rate = rate;
        }

        /// <summary>
        /// 获取运算次数。
        /// </summary>
        public int ComputeTimes { get; }

        /// <summary>
        /// 获取编辑距离。
        /// </summary>
        public int Distance { get; }

        /// <summary>
        /// 获取相似度。
        /// </summary>
        public double Rate { get; }

        /// <summary>
        /// 获取参与比较的第一个字符串。
        /// </summary>
        public string String1 { get; }

        /// <summary>
        /// 获取参与比较的第二个字符串。
        /// </summary>
        public string String2 { get; }

        #region IComparable<LevenshteinDistanceResult> Members

        /// <summary>
        /// 比较当前对象和同一类型的另一对象。
        /// </summary>
        /// <param name="other">与此对象进行比较的对象。</param>
        /// <returns>一个值，指示要比较的对象的相对顺序。</returns>
        public int CompareTo(LevenshteinDistanceResult other)
        {
            var rateComparValue = other.Rate.CompareTo(Rate);

            if (rateComparValue != 0) return rateComparValue;

            var distanceComparValue = Distance.CompareTo(other.Distance);

            if (distanceComparValue != 0) return distanceComparValue;

            var computeTimesComparValue = ComputeTimes.CompareTo(other.ComputeTimes);

            if (computeTimesComparValue != 0) return computeTimesComparValue;

            var str1ComparValue = string.CompareOrdinal(String1, other.String1);

            return str1ComparValue == 0 ? string.CompareOrdinal(String2, other.String2) : str1ComparValue;
        }

        #endregion

        #region IEquatable<LevenshteinDistanceResult> Members

        /// <summary>
        /// 指示当前对象是否等于同一类型的另一个对象。
        /// </summary>
        /// <param name="other">与此对象进行比较的对象。</param>
        /// <returns>如果当前对象等于 <paramref name="other"/> 参数，则为 true；否则为 false。</returns>
        public bool Equals(LevenshteinDistanceResult other) { return String1.Equals(other.String1) && String2.Equals(other.String2); }

        #endregion

        /// <summary>
        /// 确定指定的 <see cref="LevenshteinDistanceResult"/> 是否等于当前的 <see cref="LevenshteinDistanceResult"/>。
        /// </summary>
        /// <param name="obj">要与当前对象进行比较的对象。</param>
        /// <returns>如果指定的 <see cref="LevenshteinDistanceResult"/> 等于当前的 <see cref="LevenshteinDistanceResult"/>，则为 true；否则为 false。</returns>
        public override bool Equals(object obj) { return obj is LevenshteinDistanceResult result && Equals(result); }

        /// <summary>
        /// 返回当前对象的哈希代码。
        /// </summary>
        /// <returns>当前 <see cref="LevenshteinDistanceResult"/> 的哈希代码。</returns>
        public override int GetHashCode() { return (String1 + String2).GetHashCode(); }

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append(@"String1 = """);
            s.Append(String1);
            s.Append(@""", ");

            s.Append(@"String2 = """);
            s.Append(String2);
            s.Append(@""", ");

            s.Append(@"ComputeTimes = ");
            s.Append(ComputeTimes);
            s.Append(", ");

            s.Append(@"Distance = ");
            s.Append(Distance);
            s.Append(", ");

            s.Append(@"Rate = ");
            s.Append(Rate);

            return s.ToString();
        }
    }
}