// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System.Collections.Generic;
using System.Text;
using Yisoft.Framework.Collections.Generic;

namespace Yisoft.Framework.Security.Cryptography
{
    public class BackpackEncoder : ITokenEncoder
    {
        private static readonly BidirectionalDictionary<char, char> _Backpackage = new BidirectionalDictionary<char, char>();

        public static readonly BidirectionalDictionary<char, char> DefaultPackageSeeds = new BidirectionalDictionary<char, char>
        {
            {'0', '9'},
            {'1', '2'},
            {'2', 'e'},
            {'3', '3'},
            {'4', '4'},
            {'5', '7'},
            {'6', '8'},
            {'7', 'c'},
            {'8', 'a'},
            {'9', 'b'},
            {'a', '0'},
            {'b', '1'},
            {'c', '5'},
            {'d', '6'},
            {'e', 'd'},
            {'f', 'f'}
        };

        public BackpackEncoder(IDictionary<char, char> seeds = null) { Initialize(seeds); }

        public string Encode(string input)
        {
            if (input == null) return null;

            var result = new StringBuilder();

            foreach (var c in input) result.Append(_Backpackage[c]);

            return result.ToString();
        }

        public string Decode(string input)
        {
            if (input == null) return null;

            var result = new StringBuilder();

            foreach (var c in input) result.Append(_Backpackage.GetKey(c));

            return result.ToString();
        }

        public static void Initialize(IDictionary<char, char> seeds)
        {
            if (seeds == null || seeds.Count != 16) seeds = DefaultPackageSeeds;

            _Backpackage.Clear();

            foreach (var pair in seeds) _Backpackage.Add(pair.Key, pair.Value);
        }
    }
}