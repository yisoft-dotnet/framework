// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;

namespace Yisoft.Framework.Utilities
{
    public static class ValidationUtils
    {
        public static bool IsValidPhoneNumber(string input) { return input != null && input.Length == 11 && input.StartsWith("1", StringComparison.CurrentCulture); }

        public static bool IsValidEmailAddress(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            if (!input.Contains("@", StringComparison.InvariantCulture)) return false;
            if (!input.Contains(".", StringComparison.InvariantCulture)) return false;
            if (input.StartsWith("@", StringComparison.InvariantCulture) || input.StartsWith(".", StringComparison.InvariantCulture)) return false;
            if (input.EndsWith("@", StringComparison.InvariantCulture) || input.EndsWith(".", StringComparison.InvariantCulture)) return false;

            return !input.Contains("@.", StringComparison.InvariantCulture);
        }
    }
}