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
using Yisoft.Framework.Security.Cryptography;

namespace Yisoft.Framework.XUnitTest
{
    public class TokenHelperUnitTest
    {
        private readonly ITestOutputHelper _output;

        public TokenHelperUnitTest(ITestOutputHelper output) { _output = output; }

        private void _TokenAlgorithmTest(TokenAlgorithm tokenAlgorithm, string input, string key, string data)
        {
            for (var i = 0; i < 500; i++)
            {
                var x = tokenAlgorithm.Encrypt(input, key, data);
                var y = tokenAlgorithm.GetSalt(x);
                var z = tokenAlgorithm.Encrypt(input, key, y);

                _output.WriteLine($"{x}");
                //_output.WriteLine($"{y.ToJson()}");

                Assert.True(y.Data == data);
                Assert.True(x == z);
            }
        }

        [Theory]
        [InlineData("123456", "key", "0123")]
        public void MD5Test(string input, string key, string data)
        {
            var tokenAlgorithm = TokenHelper.MD5;

            _TokenAlgorithmTest(tokenAlgorithm, input, key, data);
        }

        [Theory]
        [InlineData("123456", "key", "0123456789ab")]
        public void SHA1Test(string input, string key, string data)
        {
            var tokenAlgorithm = TokenHelper.SHA1;

            _TokenAlgorithmTest(tokenAlgorithm, input, key, data);
        }

        [Theory]
        [InlineData("123456", "key", "0123456789abcdef")]
        public void SHA256Test(string input, string key, string data)
        {
            var tokenAlgorithm = TokenHelper.SHA256;

            _TokenAlgorithmTest(tokenAlgorithm, input, key, data);
        }

        [Theory]
        [InlineData("123456", "key", "0123456789abcdef")]
        public void SHA384Test(string input, string key, string data)
        {
            var tokenAlgorithm = TokenHelper.SHA384;

            _TokenAlgorithmTest(tokenAlgorithm, input, key, data);
        }

        [Theory]
        [InlineData("123456", "key", "0123456789abcdef")]
        public void SHA512Test(string input, string key, string data)
        {
            var tokenAlgorithm = TokenHelper.SHA512;

            _TokenAlgorithmTest(tokenAlgorithm, input, key, data);
        }
    }
}
