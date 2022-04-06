using System;
using System.Collections.Generic;
using System.Linq;

namespace CraftsApi.Helpers
{
    /// <summary>
    /// Extending the functionality of the <see cref="DateTime"/> class.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Unix Epoch operations

        /// <summary>
        /// Ticks from 01-01-0001 00:00:00.0000000 to 01-01-1970 00:00:00.0000000 (Epoch).
        /// </summary>
        public const long EpochTicks = DateTimeOffsetExtensions.EpochTicks;

        /// <summary>
        /// Unix Epoch time as a <see cref="DateTime"/> object.
        /// </summary>
        public static readonly DateTime UnixEpoch = DateTimeOffsetExtensions.UnixEpoch.UtcDateTime;

        /// <summary>
        /// Returns the amount of seconds since Unix Epoch time to the specified time
        /// </summary>
        public static long ToEpoch(this DateTime value)
        {
            // Take the current ticks and subtract the epoch ticks to get the amount of ticks since epoch.
            // Then divide the ticks by 10.000.000 (the amount of ticks in one second) to convert from ticks to seconds.
            return (value.ToUniversalTime().Ticks - EpochTicks) / TimeSpan.TicksPerSecond;
        }

        /// <summary>
        /// Returns the UTC DateTime represented by the Unix Epoch timestamp.
        /// </summary>
        public static DateTime FromEpoch(this long value)
        {
            DateTime unixEpoch = new DateTime(EpochTicks, DateTimeKind.Utc);
            return unixEpoch.AddSeconds(value);
        }

        #endregion Unix Epoch operations

        #region Difference calculations

        /// <summary>
        /// Calculate the difference between the two <see cref="DateTime"/>s in whole months.
        /// </summary>
        /// <param name="start">Start of the interval to get the difference for.</param>
        /// <param name="end">End of the interval to get the difference for.</param>
        /// <returns>The number of while months between the start and end time.</returns>
        public static int GetMonthDiff(this DateTime start, DateTime end)
        {
            return (end.Month + (end.Year * 12)) - (start.Month + (start.Year * 12));
        }

        #endregion Difference calculations

        #region Rounding operations

        /// <summary>
        /// Returns the <see cref="DateTime"/> rounded down to the nearest second.
        /// </summary>
        public static DateTime RoundToNearestSecond(this DateTime value)
        {
            return new DateTime(value.Ticks - (value.Ticks % TimeSpan.TicksPerSecond), value.Kind);
        }

        #endregion Rounding operations

        #region Working days

        /// <summary>
        /// Adds <paramref name="value"/> number of working days, skipping danish holidays and weekends.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to which to add the number of working days.</param>
        /// <param name="value">The number of working days to add.</param>
        /// <returns>A new <see cref="DateTime"/> with the number of working days added.</returns>
        public static DateTime AddWorkingDays(this DateTime date, int value)
        {
            if (value < 1)
            {
                return date;
            }

            var danishHolidays = GetDanishHolidays(date.Year).ToList();

            DateTime endDate = date;
            var counter = 0;

            do
            {
                endDate = endDate.AddDays(1);

                if (!danishHolidays.Contains(endDate) && endDate.DayOfWeek != DayOfWeek.Saturday && endDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    counter++;
                }
            }
            while (counter != value);

            return endDate;
        }

        public static IEnumerable<DateTime> GetDanishHolidays(int year)
        {
            yield return new DateTime(year, 1, 1); // New Year's Day

            var easterSunday = GetEasterSunday(year);

            yield return easterSunday.AddDays(-7); // Palm Sunday
            yield return easterSunday.AddDays(-3); // Maundy Thursday
            yield return easterSunday.AddDays(-2); // Good Friday
            yield return easterSunday; // Easter Sunday
            yield return easterSunday.AddDays(1); // Easter Monday
            yield return easterSunday.AddDays(26); // General Prayer Day
            yield return easterSunday.AddDays(39); // Ascension Day
            yield return easterSunday.AddDays(49); // Pentecost
            yield return easterSunday.AddDays(50); // Whit Monday

            yield return new DateTime(year, 12, 25); // First Day of Christmas
            yield return new DateTime(year, 12, 26); // Second Day of Christmas
        }

        // Kindly borrowed from: http://ss64.net/merlyn/estralgs.txt
        private static DateTime GetEasterSunday(int year)
        {
            var a = (year / 100 * 1483) - (year / 400 * 2225) + 2613;
            var b = ((year % 19 * 3510) + (a / 25 * 319)) / 330 % 29;
            var c = 148 - b - (((year * 5 / 4) + a - b) % 7);

            return new DateTime(year, c / 31, (c % 31) + 1);
        }

        #endregion Working days
    }
}
