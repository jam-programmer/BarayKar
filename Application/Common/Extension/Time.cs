using System.Globalization;
using System;

namespace Application.Common.Extension
{
    public static class Time
    {
        public static string PersianTime(this DateTime time)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(time);
            int month = persianCalendar.GetMonth(time);
            int day = persianCalendar.GetDayOfMonth(time);
            int hour = persianCalendar.GetHour(time);
            int minute = persianCalendar.GetMinute(time);
            int second = persianCalendar.GetSecond(time);
            var timeConverted=$"{year}/{month}/{day} {hour}:{minute}:{second}";
            return timeConverted;
        }
    }
}
 