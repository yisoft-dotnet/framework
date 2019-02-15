// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;

namespace Yisoft.Framework.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan TrimToSeconds(this TimeSpan time) { return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds); }

        public static TimeSpan TrimToMinutes(this TimeSpan time) { return new TimeSpan(time.Days, time.Hours, time.Minutes, 0); }

        public static TimeSpan TrimToHours(this TimeSpan time) { return new TimeSpan(time.Days, time.Hours, 0, 0); }

        public static TimeSpan TrimToDays(this TimeSpan time) { return new TimeSpan(time.Days, 0, 0, 0); }
    }
}