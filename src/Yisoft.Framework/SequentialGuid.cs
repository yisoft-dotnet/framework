// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Security.Cryptography;

namespace Yisoft.Framework
{
    public static class SequentialGuid
    {
        private static readonly RNGCryptoServiceProvider _RandomGenerator = new RNGCryptoServiceProvider();

        public static Guid NewGuidForMsSQL() { return Create(SequentialGuidType.SequentialAtEnd); }

        public static Guid NewGuidForMySQL() { return Create(SequentialGuidType.SequentialAsString); }

        public static Guid NewGuidForOracle() { return Create(SequentialGuidType.SequentialAsBinary); }

        public static Guid NewGuidForPgSQL() { return Create(SequentialGuidType.SequentialAsString); }

        public static Guid Create(SequentialGuidType guidType)
        {
            var randomBytes = new byte[10];

            _RandomGenerator.GetBytes(randomBytes);

            var timestamp = DateTimeOffset.UtcNow.Ticks / 10000L;
            var timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian) Array.Reverse(timestampBytes);

            var guidBytes = new byte[16];

            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }

                    break;

                case SequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }
    }
}