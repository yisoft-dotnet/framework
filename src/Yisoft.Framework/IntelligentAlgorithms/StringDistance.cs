// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework.IntelligentAlgorithms
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "<挂起>")]
    public class StringDistance
    {
        private static readonly Regex _WordsRegex = new Regex(@"([\w\d]+|\s+|.)");
        private int[,] _num;

        public int LevenshteinDistance(string strOld,
            string strNew,
            IEqualityComparer<char> comparer = null,
            Func<Choice<char>, int> weight = null,
            bool allowTransposition = false)
        {
            if (strOld == null) strOld = string.Empty;
            if (strNew == null) strNew = string.Empty;

            return LevenshteinDistance(strOld.ToCharArray(), strNew.ToCharArray(), comparer, weight, allowTransposition);
        }

        public int LevenshteinDistance<T>(T[] strOld,
            T[] strNew,
            IEqualityComparer<T> comparer = null,
            Func<Choice<T>, int> weight = null,
            bool allowTransposition = false)
        {
            if (strOld == null) strOld = Array.Empty<T>();
            if (strNew == null) strNew = Array.Empty<T>();

            var m1 = strOld.Length + 1;
            var m2 = strNew.Length + 1;

            if (comparer == null) comparer = EqualityComparer<T>.Default;

            if (weight == null) weight = c => 1;

            _ResizeArray(m1, m2);

            _num[0, 0] = 0;

            for (var i = 1; i < m1; i++) _num[i, 0] = _num[i - 1, 0] + weight(Choice<T>.Remove(strOld[i - 1]));
            for (var j = 1; j < m2; j++) _num[0, j] = _num[0, j - 1] + weight(Choice<T>.Add(strNew[j - 1]));

            for (var i = 1; i < m1; i++)
            {
                for (var j = 1; j < m2; j++)
                {
                    if (comparer.Equals(strOld[i - 1], strNew[j - 1])) _num[i, j] = _num[i - 1, j - 1];
                    else
                    {
                        _num[i, j] = Math.Min(Math
                                .Min(
                                    _num[i - 1, j] + weight(Choice<T>.Remove(strOld[i - 1])),
                                    _num[i, j - 1] + weight(Choice<T>.Add(strNew[j - 1]))
                                ),
                            _num[i - 1, j - 1] + weight(Choice<T>.Substitute(strOld[i - 1], strNew[j - 1]))
                        );

                        if (allowTransposition && i > 1
                                               && j > 1
                                               && comparer.Equals(strOld[i - 1], strNew[j - 2])
                                               && comparer.Equals(strOld[i - 2], strNew[j - 1]))
                            _num[i, j] = Math.Min(_num[i, j],
                                _num[i - 2, j - 2] + weight(Choice<T>.Transpose(strOld[i - 1], strOld[i - 2])));
                    }
                }
            }

            return _num[m1 - 1, m2 - 1];
        }

        public List<Choice<char>> LevenshteinChoices(string strOld,
            string strNew,
            IEqualityComparer<char> comparer = null,
            Func<Choice<char>, int> weight = null)
        {
            if (strOld == null) strOld = string.Empty;
            if (strNew == null) strNew = string.Empty;

            return LevenshteinChoices(strOld.ToCharArray(), strNew.ToCharArray(), comparer, weight);
        }

        public List<Choice<T>> LevenshteinChoices<T>(T[] strOld,
            T[] strNew,
            IEqualityComparer<T> comparer = null,
            Func<Choice<T>, int> weight = null)
        {
            if (comparer == null) comparer = EqualityComparer<T>.Default;

            if (weight == null) weight = c => 1;

            LevenshteinDistance(strOld, strNew, comparer, weight);

            var i = strOld.Length;
            var j = strNew.Length;

            var result = new List<Choice<T>>();

            while (i > 0 && j > 0)
            {
                if (comparer.Equals(strOld[i - 1], strNew[j - 1]))
                {
                    result.Add(Choice<T>.Equal(strOld[i - 1]));

                    i -= 1;
                    j -= 1;
                }
                else
                {
                    var cRemove = Choice<T>.Remove(strOld[i - 1]);
                    var cAdd = Choice<T>.Add(strNew[j - 1]);
                    var cSubstitute = Choice<T>.Substitute(strOld[i - 1], strNew[j - 1]);

                    var remove = _num[i - 1, j] + weight(cRemove);
                    var add = _num[i, j - 1] + weight(cAdd);
                    var substitute = _num[i - 1, j - 1] + weight(cSubstitute);

                    var min = Math.Min(remove, Math.Min(add, substitute));

                    if (substitute == min)
                    {
                        result.Add(cSubstitute);

                        i -= 1;
                        j -= 1;
                    }
                    else if (remove == min)
                    {
                        result.Add(cRemove);

                        i -= 1;
                    }
                    else if (add == min)
                    {
                        result.Add(cAdd);

                        j -= 1;
                    }
                }
            }

            while (i > 0)
            {
                result.Add(Choice<T>.Remove(strOld[i - 1]));

                i -= 1;
            }

            while (j > 0)
            {
                result.Add(Choice<T>.Add(strNew[j - 1]));

                j -= 1;
            }

            result.Reverse();

            return result;
        }

        public int LongestCommonSubstring(string str1, string str2)
        {
            return string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2)
                ? 0
                : LongestCommonSubstring(str1.ToCharArray(), str2.ToCharArray(), out _, out _);
        }

        public int LongestCommonSubstring(string str1, string str2, out int startPos1, out int startPos2)
        {
            startPos1 = 0;
            startPos2 = 0;

            return string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2)
                ? 0
                : LongestCommonSubstring(str1.ToCharArray(), str2.ToCharArray(), out startPos1, out startPos2);
        }

        public int LongestCommonSubstring<T>(T[] str1, T[] str2, out int startPos1, out int startPos2, IEqualityComparer<T> comparer = null)
        {
            if (str1 == null) throw new ArgumentNullException(nameof(str1));
            if (str2 == null) throw new ArgumentNullException(nameof(str2));

            return LongestCommonSubstring(new Slice<T>(str1), new Slice<T>(str2), out startPos1, out startPos2, comparer);
        }

        //http://en.wikibooks.org/wiki/Algorithm_Implementation/Strings/Longest_common_substring
        public int LongestCommonSubstring<T>(Slice<T> str1, Slice<T> str2, out int startPos1, out int startPos2,
            IEqualityComparer<T> comparer = null)
        {
            startPos1 = 0;
            startPos2 = 0;

            if (str1.Length == 0 || str2.Length == 0) return 0;

            if (comparer == null) comparer = EqualityComparer<T>.Default;

            _ResizeArray(str1.Length, str2.Length);

            var maxlen = 0;

            for (var i = 0; i < str1.Length; i++)
            {
                for (var j = 0; j < str2.Length; j++)
                {
                    if (!comparer.Equals(str1[i], str2[j])) _num[i, j] = 0;
                    else
                    {
                        if (i == 0 || j == 0) _num[i, j] = 1;
                        else _num[i, j] = 1 + _num[i - 1, j - 1];

                        if (_num[i, j] > maxlen)
                        {
                            maxlen = _num[i, j];
                            startPos1 = i - _num[i, j] + 1;
                            startPos2 = j - _num[i, j] + 1;
                        }
                    }
                }
            }

            return maxlen;
        }

        public int LongestCommonSubsequence(string str1, string str2)
        {
            return string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2)
                ? 0
                : LongestCommonSubsequence(str1.ToCharArray(), str2.ToCharArray());
        }

        /// <summary>
        /// ACE is a subsequence of ABCDE
        /// </summary>
        public int LongestCommonSubsequence<T>(T[] str1, T[] str2, IEqualityComparer<T> comparer = null)
        {
            if (str1 == null) throw new ArgumentNullException(nameof(str1));
            if (str2 == null) throw new ArgumentNullException(nameof(str2));

            if (str1.Length == 0 || str2.Length == 0) return 0;

            if (comparer == null) comparer = EqualityComparer<T>.Default;

            var m1 = str1.Length + 1;
            var m2 = str2.Length + 1;

            _ResizeArray(m1, m2);

            for (var i = 0; i < m1; i++) _num[i, 0] = 0;
            for (var j = 0; j < m2; j++) _num[0, j] = 0;

            for (var i = 1; i < m1; i++)
            {
                for (var j = 1; j < m2; j++)
                {
                    if (comparer.Equals(str1[i - 1], str2[j - 1])) _num[i, j] = _num[i - 1, j - 1] + 1;
                    else
                    {
                        if (_num[i, j - 1] > _num[i - 1, j]) _num[i, j] = _num[i, j - 1];
                        else _num[i, j] = _num[i - 1, j];
                    }
                }
            }

            return _num[str1.Length, str2.Length];
        }

        private void _ResizeArray(int m1, int m2)
        {
            if (_num != null && m1 <= _num.GetLength(0) && m2 <= _num.GetLength(1)) return;

            _num = new int[m1, m2];
        }

        public List<DiffPair<List<DiffPair<string>>>> DiffText(string textOld, string textNew, bool lineEndingDifferences = true)
        {
            if (textOld == null) textOld = string.Empty;
            if (textNew == null) textNew = string.Empty;

            var linesOld = lineEndingDifferences ? textOld.Split('\n') : textOld.Lines();
            var linesNew = lineEndingDifferences ? textNew.Split('\n') : textNew.Lines();

            var diff = Diff(linesOld, linesNew);

            var groups = diff.GroupWhenChange(a => a.Action == DiffAction.Equal).ToList();

            var sd = new StringDistance();

            return groups.SelectMany(g =>
            {
                if (g.Key) return g.Select(dp => new DiffPair<List<DiffPair<string>>>(DiffAction.Equal, new List<DiffPair<string>> {dp}));

                var removed = g.Where(a => a.Action == DiffAction.Removed).Select(a => a.Value).ToArray();
                var added = g.Where(a => a.Action == DiffAction.Added).Select(a => a.Value).ToArray();

                var choices = LevenshteinChoices(removed, added, weight: c =>
                {
                    if (c.Type == ChoiceType.Add) return c.Added.Length;

                    if (c.Type == ChoiceType.Remove) return c.Removed.Length;

                    var distance = sd.LevenshteinDistance(c.Added, c.Removed);

                    return distance * 2;
                });

                return choices.Select(c =>
                {
                    if (c.Type == ChoiceType.Add)
                    {
                        return new DiffPair<List<DiffPair<string>>>(DiffAction.Added,
                            new List<DiffPair<string>> {new DiffPair<string>(DiffAction.Added, c.Added)});
                    }

                    if (c.Type == ChoiceType.Remove)
                    {
                        return new DiffPair<List<DiffPair<string>>>(DiffAction.Removed,
                            new List<DiffPair<string>> {new DiffPair<string>(DiffAction.Removed, c.Removed)});
                    }

                    var diffWords = sd.DiffWords(c.Removed, c.Added);

                    return new DiffPair<List<DiffPair<string>>>(DiffAction.Equal, diffWords);
                });
            }).ToList();
        }

        public List<DiffPair<string>> DiffWords(string strOld, string strNew)
        {
            var wordsOld = _WordsRegex.Matches(strOld).Select(m => m.Value).ToArray();
            var wordsNew = _WordsRegex.Matches(strNew).Select(m => m.Value).ToArray();

            return Diff(wordsOld, wordsNew);
        }

        public List<DiffPair<T>> Diff<T>(T[] strOld, T[] strNew, IEqualityComparer<T> comparer = null)
        {
            var result = new List<DiffPair<T>>();

            _DiffPrivate(new Slice<T>(strOld), new Slice<T>(strNew), comparer, result);

            return result;
        }

        private void _DiffPrivate<T>(Slice<T> sliceOld, Slice<T> sliceNew, IEqualityComparer<T> comparer, List<DiffPair<T>> result)
        {
            var length = LongestCommonSubstring(sliceOld, sliceNew, out var posOld, out var posNew, comparer);

            if (length == 0)
            {
                _AddResults(result, sliceOld, DiffAction.Removed);
                _AddResults(result, sliceNew, DiffAction.Added);
            }
            else
            {
                _TryDiff(sliceOld.SubSliceStart(posOld), sliceNew.SubSliceStart(posNew), comparer, result);

                _AddResults(result, sliceOld.SubSlice(posOld, length), DiffAction.Equal);

                _TryDiff(sliceOld.SubSliceEnd(posOld + length), sliceNew.SubSliceEnd(posNew + length), comparer, result);
            }
        }

        private static void _AddResults<T>(List<DiffPair<T>> list, Slice<T> slice, DiffAction action) { list.AddRange(slice.Select(t => new DiffPair<T>(action, t))); }

        private void _TryDiff<T>(Slice<T> sliceOld, Slice<T> sliceNew, IEqualityComparer<T> comparer, List<DiffPair<T>> result)
        {
            if (sliceOld.Length > 0 && sliceOld.Length > 0) _DiffPrivate(sliceOld, sliceNew, comparer, result);
            else if (sliceOld.Length > 0) _AddResults(result, sliceOld, DiffAction.Removed);
            else if (sliceNew.Length > 0) _AddResults(result, sliceNew, DiffAction.Added);
        }
    }
}