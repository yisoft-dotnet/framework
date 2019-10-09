// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;

namespace Yisoft.Framework.Utilities
{
    public static class DateTimeUtils
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static readonly Func<DateTime> UtcNowFunc = () => DateTime.UtcNow;

        public static DateTime UtcNow => UtcNowFunc();

        public static int ToTimeStamp(DateTime dateTime) { return (int) (dateTime.ToUniversalTime() - UnixEpoch).TotalSeconds; }

        public static DateTime FromTimeStamp(int timestamp) { return UnixEpoch.AddSeconds(timestamp).ToLocalTime(); }
    }
}