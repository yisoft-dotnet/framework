// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System.IO;

namespace Yisoft.Framework.Extensions
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, string value, bool endWith0)
        {
            if (writer == null) return;

            if (!endWith0)
            {
                writer.Write(value);

                return;
            }

            if (!string.IsNullOrEmpty(value)) writer.Write(value.ToCharArray());

            writer.Write((byte) 0);
        }
    }
}