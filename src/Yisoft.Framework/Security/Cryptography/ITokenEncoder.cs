// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

namespace Yisoft.Framework.Security.Cryptography
{
    public interface ITokenEncoder
    {
        string Decode(string input);

        string Encode(string input);
    }
}