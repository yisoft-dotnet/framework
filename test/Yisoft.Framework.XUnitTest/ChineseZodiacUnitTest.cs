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
using Xunit;
using Xunit.Abstractions;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework.XUnitTest
{
    public class ChineseZodiacUnitTest
    {
        private readonly ITestOutputHelper _output;

        public ChineseZodiacUnitTest(ITestOutputHelper output) { _output = output; }

        [Theory]
        [InlineData(2017, ChineseZodiac.Cock)]
        [InlineData(2018, ChineseZodiac.Dog)]
        [InlineData(2019, ChineseZodiac.Boar)]
        [InlineData(2020, ChineseZodiac.Rat)]
        [InlineData(2021, ChineseZodiac.Ox)]
        [InlineData(2022, ChineseZodiac.Tiger)]
        [InlineData(2023, ChineseZodiac.Hare)]
        [InlineData(2024, ChineseZodiac.Dragon)]
        [InlineData(2025, ChineseZodiac.Snake)]
        [InlineData(2026, ChineseZodiac.Horse)]
        [InlineData(2027, ChineseZodiac.Sheep)]
        [InlineData(2028, ChineseZodiac.Monkey)]
        [InlineData(2029, ChineseZodiac.Cock)]
        [InlineData(2030, ChineseZodiac.Dog)]
        public void ChineseZodiacTest(int year, ChineseZodiac zodiac)
        {
            var x = new DateTime(year, 7, 19).GetChineseZodiac();

            _output.WriteLine("{0}, {1}, {2}", year, zodiac, x);

            Assert.True(x == zodiac);
        }
    }
}
