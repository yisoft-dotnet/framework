// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;

namespace Yisoft.Framework
{
    public static class HostingEnvironments
    {
        public const string Local = "local";

        public const string Development = "development";

        public const string Test = "test";

        public const string Staging = "staging";

        public const string Production = "production";

        public const string Demonstration = "demonstration";

        public static string NormalizeEnvName(string envName, string defaultValue = Local)
        {
            if (string.IsNullOrWhiteSpace(envName)) return defaultValue ?? string.Empty;

            return envName.ToLower();
        }

        public static string Postfix(string input, string envName, string hiddenValue = Local, string separator = "_")
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var suffix = NormalizeEnvName(envName, hiddenValue);

            separator = string.IsNullOrEmpty(separator) ? "_" : separator;
            suffix = suffix == hiddenValue ? string.Empty : separator + suffix;

            return $"{input}{suffix}";
        }
    }
}