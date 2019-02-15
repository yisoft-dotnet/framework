// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

namespace Yisoft.Framework.Utilities
{
    public static class ValidationUtils
    {
        public static bool IsValidPhoneNumber(string input) { return input != null && input.Length == 11 && input.StartsWith("1"); }

        public static bool IsValidEmailAddress(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            if (!input.Contains("@")) return false;
            if (!input.Contains(".")) return false;
            if (input.StartsWith("@") || input.StartsWith(".")) return false;
            if (input.EndsWith("@") || input.EndsWith(".")) return false;

            return !input.Contains("@.");
        }
    }
}