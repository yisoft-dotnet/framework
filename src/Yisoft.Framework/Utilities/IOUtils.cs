// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Yisoft.Framework.Utilities
{
    /// <summary>
    /// 封装了对路径字符串的常用操作。
    /// </summary>
    public static class IOUtils
    {
        /// <summary>
        /// 正则表达式选项。
        /// </summary>
        private const RegexOptions _REGEX_OPTIONS = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline;

        /// <summary>
        /// 按照指定的文件路径创建文件夹和子文件夹，可包含文件名。
        /// </summary>
        /// <param name="path">指定要创建的路径。</param>
        /// <returns>创建成功后将返回指定的文件路径。</returns>
        public static string CreateDirectories(string path)
        {
            try
            {
                var fp = Path.GetDirectoryName(path) ?? string.Empty;

                fp = fp.TrimEnd('/', '\\') + "\\";

                return Directory.CreateDirectory(fp).FullName + Path.GetFileName(path);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 确定指定的文件夹是否存在。
        /// </summary>
        /// <param name="path">要检查的文件夹的物理路径。</param>
        /// <returns>bool</returns>
        public static bool Exists(string path) { return Directory.Exists(path); }

        /// <summary>
        /// 确定指定的文件夹是否存在，如果存在则检查其创建时间是否已经过期。
        /// </summary>
        /// <param name="path">要检查的文件夹的物理路径。</param>
        /// <param name="expires">过期时间，单位为秒。</param>
        /// <returns>如果文件夹不存在或文件夹已经过期则返回 false 。</returns>
        public static bool Exists(string path, int expires)
        {
            return Directory.Exists(path) && Directory.GetLastWriteTime(path).AddSeconds(expires).Ticks - DateTime.Now.Ticks > 0;
        }

        /// <summary>
        /// 确定指定的文件夹是否存在，如果存在则检查其创建时间是否已经过期。
        /// </summary>
        /// <param name="path">要检查的文件夹的物理路径。</param>
        /// <param name="expires">过期时间。</param>
        /// <returns>如果文件夹不存在或文件夹已经过期则返回 false 。</returns>
        public static bool Exists(string path, TimeSpan expires) { return Exists(path, (int) expires.TotalSeconds); }

        /// <summary>
        /// 确定指定的文件是否存在。
        /// </summary>
        /// <param name="path">要检查的文件夹的物理路径。</param>
        /// <returns>bool</returns>
        public static bool FileExists(string path) { return File.Exists(path); }

        /// <summary>
        /// 确定指定的文件是否存在，如果存在则检查其创建时间是否已经过期。
        /// </summary>
        /// <param name="fileName">要检查的文件的物理路径。</param>
        /// <param name="expires">过期时间，单位为秒。</param>
        /// <returns>如果文件不存在或文件已经过期则返回 false 。</returns>
        public static bool FileExists(string fileName, int expires)
        {
            return File.Exists(fileName) && File.GetLastWriteTime(fileName).AddSeconds(expires).Ticks - DateTime.Now.Ticks > 0;
        }

        /// <summary>
        /// 确定指定的文件是否存在，如果存在则检查其创建时间是否已经过期。
        /// </summary>
        /// <param name="path">要检查的文件的物理路径。</param>
        /// <param name="expires">过期时间。</param>
        /// <returns>如果文件不存在或文件已经过期则返回 false 。</returns>
        public static bool FileExists(string path, TimeSpan expires) { return FileExists(path, (int) expires.TotalSeconds); }

        /// <summary>
        /// 将现有文件复制到新文件。覆盖同名的文件。
        /// </summary>
        /// <param name="sourceFileName">要复制的文件。</param>
        /// <param name="destFileName">目标文件的名称。不能是目录。</param>
        public static void CopyFile(string sourceFileName, string destFileName) { CopyFile(sourceFileName, destFileName, true); }

        /// <summary>
        /// 将现有文件复制到新文件。允许覆盖同名的文件。
        /// </summary>
        /// <param name="sourceFileName">要复制的文件。</param>
        /// <param name="destFileName">目标文件的名称。不能是目录。</param>
        /// <param name="overwrite">如果可以覆盖目标文件，则为 true；否则为 false。</param>
        public static void CopyFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (File.Exists(sourceFileName) == false) return;

            try
            {
                File.Copy(sourceFileName, destFileName, overwrite);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 删除指定的目录并删除该目录中的任何子目录。
        /// </summary>
        /// <param name="path">要移除的目录的名称。</param>
        /// <returns>如果成功删除了指定的目录则返回 true，如果删除失败或发生异常则返回 false。</returns>
        public static bool DeleteDirectory(string path) { return DeleteDirectory(path, true); }

        /// <summary>
        /// 删除指定的目录并（如果指示）删除该目录中的任何子目录。
        /// </summary>
        /// <param name="path">要移除的目录的名称。</param>
        /// <param name="recursive">若要移除 path 中的目录、子目录和文件，则为 true；否则为 false。</param>
        /// <returns>如果成功删除了指定的目录则返回 true，如果删除失败或发生异常则返回 false。</returns>
        public static bool DeleteDirectory(string path, bool recursive)
        {
            if (Directory.Exists(path) == false) return true;

            try
            {
                Directory.Delete(path, recursive);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定目录中的任何文件、文件夹和子文件夹，但不删除自身。
        /// </summary>
        /// <param name="path">要清空的目录的名称。</param>
        /// <returns>如果指定的目录被清空则返回 true，如果清空失败或发生异常则返回 false。</returns>
        public static bool ClearDirectory(string path)
        {
            if (Directory.Exists(path) == false) return false;

            try
            {
                var folders = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

                foreach (var folder in folders) Directory.Delete(folder, true);

                var files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);

                foreach (var file in files) File.Delete(file);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修正路径，去掉路径中重复的分隔符和最前面的分隔符，并确保路径最终以分隔符结尾。
        /// </summary>
        /// <param name="path">要整理的路径。</param>
        /// <param name="seperator">路径分隔符。</param>
        /// <returns>string</returns>
        public static string FixPath(string path, char seperator)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            path = RemoveDoubleSeperators(seperator, path.Replace('\\', seperator).Replace('/', seperator));
            path = path.Trim(seperator);

            if (!string.IsNullOrEmpty(path)) path += seperator;

            return path;
        }

        /// <summary>
        /// 格式化路径。
        /// </summary>
        /// <param name="path">主路径。</param>
        /// <param name="appPath">要拼合的路径。</param>
        /// <returns>格式化之后的路径字符串。</returns>
        public static string FormatPath(string path, string appPath)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            if (path.StartsWith(appPath, StringComparison.CurrentCulture)) return path;

            if (path[0] != '/' && path[0] != '\\') path = "/" + path;

            return appPath + path;
        }

        /// <summary>
        /// 删除重复的路径分隔符。
        /// </summary>
        /// <param name="seperator">路径分隔符。</param>
        /// <param name="path">要处理的路径。</param>
        /// <returns>已经删除重复分隔符的路径字符串。</returns>
        public static string RemoveDoubleSeperators(char seperator, string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            var s = seperator.ToString();
            var d = s + s;

            while (path.IndexOf(d, StringComparison.Ordinal) != -1) path = path.Replace(d, s, StringComparison.CurrentCulture);

            return path;
        }

        /// <summary>
        /// 返回虚拟路径的目录部分。
        /// </summary>
        /// <param name="path">虚拟路径</param>
        /// <returns>虚拟路径中引用的目录。</returns>
        public static string GetDirectoryPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            return Regex.Replace(path.Replace("\\", "/", StringComparison.CurrentCulture), "[^/]+?$", string.Empty, _REGEX_OPTIONS);
        }

        /// <summary>
        /// 返回指定路径字符串的目录信息。
        /// </summary>
        /// <param name="path">文件或目录的路径。</param>
        /// <returns>包含 path 目录信息的 System.String；或者为 null（如果 path 表示根目录、是空字符串 ("") 或是 null）。如果 path 没有包含目录信息，则返回 System.String.Empty。</returns>
        public static string GetDirectoryName(string path) { return Path.GetDirectoryName(path); }

        /// <summary>
        /// 将一个基路径和一个相对路径进行组合。
        /// </summary>
        /// <param name="basePath">基路径。</param>
        /// <param name="relativePath">相对路径。</param>
        /// <param name="autoCreatePath">如果路径合并成功，是否自动创建文件夹层次。</param>
        /// <returns>组合后的 basePath 和 relativePath。</returns>
        public static string Combine(string basePath, string relativePath, bool autoCreatePath = false)
        {
            var path = Path.Combine(basePath, relativePath);

            if (autoCreatePath) path = CreateDirectories(path);

            return path;
        }
    }
}