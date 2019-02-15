// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.IO;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// make load config files simpler.
    /// </summary>
    /// <example>
    /// new ConfigurationBuilder()
    ///     .Scan((builder, file) => builder.AddYamlFile(file, true, true),
    ///         "compiler/config",
    ///         @"/([^.]+\.yml)$",
    ///         $@"/([^.]+\.{envName}\.yml)$"
    ///     );
    /// </example>
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder Scan(this IConfigurationBuilder builder,
            Action<IConfigurationBuilder, string> configAction, string path, params string[] searchPatterns)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (searchPatterns == null || searchPatterns.Length == 0) throw new ArgumentNullException(nameof(searchPatterns));

            path = path.Replace('\\', '/');

            foreach (var searchPattern in searchPatterns)
                Scan(builder, configAction, path, searchPattern);

            return builder;
        }

        public static IConfigurationBuilder Scan(this IConfigurationBuilder builder,
            Action<IConfigurationBuilder, string> configAction, string path, string searchPattern)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(searchPattern)) throw new ArgumentNullException(nameof(searchPattern));

            var regex = new Regex(searchPattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var directory = new DirectoryInfo(path);
            var fileInfos = directory.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (var fileInfo in fileInfos)
            {
                var fullName = fileInfo.FullName.Replace('\\', '/');
                var fileName = fullName.Substring(fullName.IndexOf(path, StringComparison.OrdinalIgnoreCase));

                if (regex.IsMatch(fileName)) configAction?.Invoke(builder, fileName);
            }

            return builder;
        }
    }
}