namespace SchoolIsComingSoon.Application.Extensions
{
    static class DateTimeFormatExtensions
    {
        public static string ToPostFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMM yyyy").Replace(".", "");
        }

        public static string ToCommentFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMM yyyy в HH:mm").Replace(".", "");
        }
    }
}