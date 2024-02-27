using System;

namespace MomsNotebook.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime FormatDateTime(this DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }

        public static string FormatMySqlDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
