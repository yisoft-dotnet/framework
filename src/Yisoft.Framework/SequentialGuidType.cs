// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

namespace Yisoft.Framework
{
    public enum SequentialGuidType
    {
        /*
            Database                GUID Column         SequentialGuidType Value
            Microsoft SQL Server    uniqueidentifier    SequentialAtEnd
            MySQL                   char(36)            SequentialAsString
            Oracle                  raw(16)             SequentialAsBinary
            PostgreSQL              uuid                SequentialAsString
            SQLite                  varies              varies
         */

        SequentialAsString,
        SequentialAsBinary,
        SequentialAtEnd
    }
}