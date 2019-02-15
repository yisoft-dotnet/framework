// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using Yisoft.Framework.Extensions;

namespace Yisoft.Framework
{
    public class EnumExtraInfo
    {
        public EnumExtraInfo() { }

        public EnumExtraInfo(string title, string name, long value)
            : this(title, name, value, value)
        {
        }

        public EnumExtraInfo(string title, string name, long value, long rank)
        {
            Title = title == name ? title.ToCamelCase() : title;
            Name = name.ToCamelCase();
            Value = value;
            Rank = rank == 0 ? null : Rank;
        }

        public string Title { get; }

        public string Name { get; }

        public long Value { get; }

        public long? Rank { get; }
    }
}