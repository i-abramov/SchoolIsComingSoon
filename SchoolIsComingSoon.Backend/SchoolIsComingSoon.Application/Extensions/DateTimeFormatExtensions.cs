using System.Globalization;

namespace SchoolIsComingSoon.Application.Extensions
{
    static class DateTimeFormatExtensions
    {
        public static string ToPostFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMM yyyy", new CultureInfo("ru-RU")).Replace(".", "");
        }

        public static string ToCommentFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMM yyyy в HH:mm", new CultureInfo("ru-RU")).Replace(".", "");
        }
    }
}