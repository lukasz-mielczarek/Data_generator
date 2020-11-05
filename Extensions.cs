using System;

namespace data_generator
{
    public static class Extensions
    {
        public static string ToDateString(this DateTime date, char separator = '-')
        {
            return $"{date.Day}{separator}{date.Month}{separator}{date.Year}";
        }

        public static DateTime StringToDate(this string date, char separator = '-')
        {
            string[] splitDate = date.Split(separator);

            int day = int.Parse(splitDate[0]);
            int month = int.Parse(splitDate[1]);
            int year = int.Parse(splitDate[2]);

            return new DateTime(year, month, day);
        }
    }
}