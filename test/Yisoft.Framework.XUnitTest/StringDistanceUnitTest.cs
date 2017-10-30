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

using Xunit;
using Xunit.Abstractions;
using Yisoft.Framework.Extensions;
using Yisoft.Framework.IntelligentAlgorithms;

namespace Yisoft.Framework.XUnitTest
{
    public class StringDistanceUnitTest
    {
        public StringDistanceUnitTest(ITestOutputHelper output) { _output = output; }
        private readonly ITestOutputHelper _output;
        private readonly StringDistance _stringDistance = new StringDistance();

        [Fact]
        public void Choices()
        {
            var result = _stringDistance.LevenshteinChoices("en un lugar de la mancha".ToCharArray(), "in un legarito de la mincha".ToCharArray());

            var str = result.ToString(string.Empty);

            _output.WriteLine(str);

            Assert.True("[-e+i]n un l[-u+e]gar+i+t+o de la m[-a+i]ncha" == str);
        }

        [Fact]
        public void Diff()
        {
            var result = _stringDistance.Diff("en un lugar de la mancha".ToCharArray(), "in un place de la mincha".ToCharArray());

            var str = result.ToString(string.Empty);

            _output.WriteLine(str);

            Assert.True("-e+in un +pl-u-ga-r+c+e de la m-a+incha" == str);
        }

        [Fact]
        public void DiffText()
        {
            const string text1 = @"  Hola Pedro
Que tal
Bien

Adios Juan";

            const string text2 = @"  Hola Pedri

Que til
Adios Juani";

            var result = _stringDistance.DiffText(text1, text2);

            var str = result.ToString(l => (l.Action == DiffAction.Added
                                               ? "[+]"
                                               : l.Action == DiffAction.Removed
                                                   ? "[-]"
                                                   : "[=]")
                                           + l.Value.ToString(string.Empty), "\n");

            _output.WriteLine(str);

            Assert.True(
                @"[=]  Hola -Pedro+Pedri
[-]-Que tal
[-]-Bien
[=]
[+]+Que til
[=]Adios -Juan+Juani" == str);
        }

        [Fact]
        public void DiffWords()
        {
            var result = _stringDistance.DiffWords(
                "Soft drinks, coffees, teas, beers, and ginger ales",
                "Soft drinks, coffees, teas and beers");

            var str = result.ToString(string.Empty);

            _output.WriteLine(str);

            Assert.True("Soft drinks, coffees, teas-,- -beers-, and -ginger- -ales+beers" == str);
        }

        [Fact]
        public void LevenshteinDistance()
        {
            Assert.True(1 == _stringDistance.LevenshteinDistance("hi", "ho"));
            Assert.True(1 == _stringDistance.LevenshteinDistance("hi", "hil"));
            Assert.True(1 == _stringDistance.LevenshteinDistance("hi", "h"));
        }

        [Fact]
        public void LevenshteinDistanceWeight()
        {
            int Func(Choice<char> c) => c.HasAdded && char.IsNumber(c.Added) || c.HasRemoved && char.IsNumber(c.Removed) ? 10 : 1;

            Assert.True(10 == _stringDistance.LevenshteinDistance("hola", "ho5la", weight: Func));
            Assert.True(10 == _stringDistance.LevenshteinDistance("ho5la", "hola", weight: Func));
            Assert.True(10 == _stringDistance.LevenshteinDistance("ho5la", "hojla", weight: Func));
            Assert.True(10 == _stringDistance.LevenshteinDistance("hojla", "ho5la", weight: Func));
            Assert.True(10 == _stringDistance.LevenshteinDistance("ho5la", "ho6la", weight: Func));
        }

        [Fact]
        public void LongestCommonSubsequence()
        {
            Assert.True(4 == _stringDistance.LongestCommonSubsequence("hallo", "halo"));
            Assert.True(7 == _stringDistance.LongestCommonSubsequence("SupeMan", "SuperMan"));
            Assert.True(0 == _stringDistance.LongestCommonSubsequence("aoa", string.Empty));
        }

        [Fact]
        public void LongestCommonSubstring()
        {
            Assert.True(3 == _stringDistance.LongestCommonSubstring("hallo", "halo"));
            Assert.True(4 == _stringDistance.LongestCommonSubstring("SupeMan", "SuperMan"));
            Assert.True(0 == _stringDistance.LongestCommonSubstring("aoa", string.Empty));
        }
    }
}
