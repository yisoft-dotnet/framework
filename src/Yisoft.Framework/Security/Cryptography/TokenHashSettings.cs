// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System.Security.Cryptography;

namespace Yisoft.Framework.Security.Cryptography
{
    public class TokenHashSettings
    {
        public static readonly TokenHashSettings MD5 = new TokenHashSettings
        {
            Version = 1,
            HashAlgorithm = HashAlgorithmName.MD5,

            VersionLength = 7,
            VersionPosStart = 3,
            VersionPosEnd = 12,

            SaltLength = 4,
            SaltPosStart = 13,
            SaltPosEnd = 17,

            // max data length: 4
            DataMaxlength = 12,
            DataPosStart = 18,
            DataPosEnd = 31
        };

        public static readonly TokenHashSettings SHA1 = new TokenHashSettings
        {
            Version = 2,
            HashAlgorithm = HashAlgorithmName.SHA1,

            VersionLength = 7,
            VersionPosStart = 3,
            VersionPosEnd = 12,

            SaltLength = 4,
            SaltPosStart = 13,
            SaltPosEnd = 18,

            // max data length: 12
            DataMaxlength = 20,
            DataPosStart = 19,
            DataPosEnd = 39
        };

        public static readonly TokenHashSettings SHA256 = new TokenHashSettings
        {
            Version = 3,
            HashAlgorithm = HashAlgorithmName.SHA256,

            VersionLength = 7,
            VersionPosStart = 3,
            VersionPosEnd = 12,

            SaltLength = 8,
            SaltPosStart = 13,
            SaltPosEnd = 24,

            // max data length: 24
            DataMaxlength = 32,
            DataPosStart = 25,
            DataPosEnd = 61
        };

        public static readonly TokenHashSettings SHA384 = new TokenHashSettings
        {
            Version = 3,
            HashAlgorithm = HashAlgorithmName.SHA384,

            VersionLength = 7,
            VersionPosStart = 3,
            VersionPosEnd = 12,

            SaltLength = 8,
            SaltPosStart = 13,
            SaltPosEnd = 25,

            // max data length: 56
            DataMaxlength = 64,
            DataPosStart = 26,
            DataPosEnd = 95
        };

        public static readonly TokenHashSettings SHA512 = new TokenHashSettings
        {
            Version = 4,
            HashAlgorithm = HashAlgorithmName.SHA512,

            VersionLength = 7,
            VersionPosStart = 3,
            VersionPosEnd = 12,

            SaltLength = 8,
            SaltPosStart = 13,
            SaltPosEnd = 25,

            // max data length: 88
            DataMaxlength = 96,
            DataPosStart = 26,
            DataPosEnd = 126
        };

        public HashAlgorithmName HashAlgorithm { get; set; }

        public int Version { get; set; }

        public int VersionLength { get; set; }

        public int VersionPosStart { get; set; }

        public int VersionPosEnd { get; set; }

        public int SaltLength { get; set; }

        public int SaltPosStart { get; set; }

        public int SaltPosEnd { get; set; }

        public int DataMaxlength { get; set; }

        public int DataPosStart { get; set; }

        public int DataPosEnd { get; set; }
    }
}