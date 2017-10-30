//      )                             *     
//   ( /(        *   )       (      (  `    
//   )\()) (   ` )  /( (     )\     )\))(   
//  ((_)\  )\   ( )(_)))\ ((((_)(  ((_)()\  
// __ ((_)((_) (_(_())((_) )\ _ )\ (_()((_) 
// \ \ / / (_) |_   _|| __|(_)_\(_)|  \/  | 
//  \ V /  | | _ | |  | _|  / _ \  | |\/| | 
//   |_|   |_|(_)|_|  |___|/_/ \_\ |_|  |_| 
// 
// This file is subject to the terms and conditions defined in
// file 'License.txt', which is part of this source code package.
// 
// Copyright © Yi.TEAM. All rights reserved.
// -------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Linq;
using Yisoft.Framework.Utilities;

namespace Yisoft.Framework.Extensions
{
    /// <summary>
    /// 为 <see cref="DateTime"/> 对象提供扩展方法。
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 返回与当前 <see cref="DateTime"/> 关联的属相信息。
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/></param>
        /// <returns>返回 <see cref="ChineseZodiac"/> 枚举值之一。</returns>
        public static ChineseZodiac GetChineseZodiac(this DateTime dateTime)
        {
            var v = dateTime.Year % 12 + 8;

            v = v > 11 ? v % 12 : v;

            return (ChineseZodiac) (v + 1);
        }

        /// <summary>
        /// 返回与当前 <see cref="DateTime"/> 关联的星座信息。
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/></param>
        /// <returns>返回 <see cref="Constellation"/> 枚举值之一。</returns>
        public static Constellation GetStarSign(this DateTime dateTime)
        {
            return EnumHelper.StarSignsVictor.Descriptions.Where(
                    desc => desc.Value.StartMonth == dateTime.Month && dateTime.Day >= desc.Value.StartDay
                            || desc.Value.EndMonth == dateTime.Month && dateTime.Day <= desc.Value.EndDay)
                .Select(desc => desc.Key)
                .FirstOrDefault();
        }

        /// <summary>
        /// 返回当前 <see cref="DateTime"/> 与 <see cref="DateTime.Now"/> 的时间差。
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/></param>
        /// <returns>返回 <see cref="TimeSpan"/>。</returns>
        public static TimeSpan GetDateDiffOfNow(this DateTime dateTime)
        {
            return DateTime.Now - dateTime;
        }

        /// <summary>
        /// 根据当前 <see cref="DateTime"/> 与指定的表示生日的 <see cref="DateTime"/> 返回年龄。
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/></param>
        /// <param name="birthday">指定表示生日的 <see cref="DateTime"/>。</param>
        /// <returns>返回 <see cref="Int32"/>。</returns>
        public static int CalculateAge(this DateTime dateTime, DateTime birthday)
        {
            var age = DateTime.Now.Year - birthday.Year;

            return DateTime.Now.Month < birthday.Month
                   || DateTime.Now.Month == birthday.Month && DateTime.Now.Day < birthday.Day
                ? --age
                : age;
        }

        [DebuggerStepThrough]
        public static bool HasExceeded(this DateTime time, int seconds)
        {
            return DateTimeUtils.UtcNow > time.AddSeconds(seconds);
        }

        [DebuggerStepThrough]
        public static int GetLifetimeInSeconds(this DateTime time)
        {
            return (int) (DateTimeUtils.UtcNow - time).TotalSeconds;
        }

        [DebuggerStepThrough]
        public static bool HasExpired(this DateTime? expirationTime)
        {
            return expirationTime.HasValue && expirationTime.Value.HasExpired();
        }

        [DebuggerStepThrough]
        public static bool HasExpired(this DateTime expirationTime)
        {
            return expirationTime < DateTimeUtils.UtcNow;
        }

        public static DateTime AccurateToMinute(this DateTime d) { return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0); }

        public static DateTime AccurateTo10Minute(this DateTime d)
        {
            var minute = 0;

            if (d.Minute < 10) minute = 0;
            else if (d.Minute < 20) minute = 10;
            else if (d.Minute < 30) minute = 20;
            else if (d.Minute < 40) minute = 30;
            else if (d.Minute < 50) minute = 40;
            else if (d.Minute < 60) minute = 50;

            return new DateTime(d.Year, d.Month, d.Day, d.Hour, minute, 0);
        }

        public static DateTime AccurateTo15Minute(this DateTime d)
        {
            var minute = 0;

            if (d.Minute < 15) minute = 0;
            else if (d.Minute < 30) minute = 15;
            else if (d.Minute < 45) minute = 30;
            else if (d.Minute < 60) minute = 45;

            return new DateTime(d.Year, d.Month, d.Day, d.Hour, minute, 0);
        }

        public static DateTime AccurateTo30Minute(this DateTime d)
        {
            var minute = d.Minute <= 30 ? 0 : 30;

            return new DateTime(d.Year, d.Month, d.Day, d.Hour, minute, 0);
        }

        public static DateTime AccurateToHour(this DateTime d) { return new DateTime(d.Year, d.Month, d.Day, d.Hour, 0, 0); }

        public static int ToTimestamp(this DateTime d) { return DateTimeUtils.ToTimeStamp(d); }
    }
}
