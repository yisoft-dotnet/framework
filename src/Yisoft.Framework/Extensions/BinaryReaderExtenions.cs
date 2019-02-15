// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System.IO;
using System.Text;

namespace Yisoft.Framework.Extensions
{
    public static class BinaryReaderExtenions
    {
        public static string ReadString(this BinaryReader reader, bool endWith0)
        {
            if (!endWith0) return reader.ReadString();

            var contentBuilder = new StringBuilder();

            char c;

            while ((c = reader.ReadChar()) > char.MinValue) contentBuilder.Append(c);

            return contentBuilder.ToString();
        }
    }
}