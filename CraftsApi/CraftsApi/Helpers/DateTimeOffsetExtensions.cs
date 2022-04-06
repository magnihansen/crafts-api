using System;
namespace CraftsApi.Helpers
{
    /// <summary>
    /// Extending the functionality of the <see cref="DateTimeOffset"/> class.
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        #region Unix Epoch operations

        /// <summary>
        /// Ticks from 01-01-0001 00:00:00.0000000 to 01-01-1970 00:00:00.0000000 (Epoch).
        /// </summary>
        public const long EpochTicks = 621355968000000000;

        /// <summary>
        /// Unix Epoch time as a <see cref="DateTimeOffset"/> object.
        /// </summary>
        public static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(EpochTicks, TimeSpan.Zero);

        /// <summary>
        /// Returns the amount of seconds since Unix Epoch time to the specified time
        /// </summary>
        public static long ToEpoch(this DateTimeOffset value)
        {
            // Take the current ticks and subtract the epoch ticks to get the amount of ticks since epoch.
            // Then divide the ticks by 10.000.000 (the amount of ticks in one second) to convert from ticks to seconds.
            return (value.ToUniversalTime().Ticks - EpochTicks) / TimeSpan.TicksPerSecond;
        }

        /// <summary>
        /// Returns the UTC DateTimeOffset represented by the Unix Epoch timestamp.
        /// </summary>
        public static DateTimeOffset FromEpochAsDateTimeOffset(this long value)
            => UnixEpoch.AddSeconds(value);

        #endregion

        #region Difference calculations

        /// <summary>
        /// Calculate the difference between the two <see cref="DateTimeOffset"/>s in whole months.
        /// </summary>
        /// <param name="start">Start of the interval to get the difference for.</param>
        /// <param name="end">End of the interval to get the difference for.</param>
        /// <returns>The number of while months between the start and end time.</returns>
        public static int GetMonthDiff(this DateTimeOffset start, DateTimeOffset end)
        {
            return (end.Month + end.Year * 12) - (start.Month + start.Year * 12);
        }

        #endregion

        #region Rounding operations

        /// <summary>
        /// Returns the <see cref="DateTimeOffset"/> rounded down to the nearest second.
        /// </summary>
        public static DateTimeOffset RoundToNearestSecond(this DateTimeOffset value)
        {
            return new DateTimeOffset(value.Ticks - (value.Ticks % TimeSpan.TicksPerSecond), value.Offset);
        }

        #endregion

    }
}
