using System.Globalization;

namespace MyInfrastructure.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ToEgyptionDate(this string date)
        {
            DateTime dt;

            var IsConverted = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out dt);
            if (!IsConverted)
            {
                dt = DateTime.Parse(date);
            }
            return dt;
        }

        public static string ToEgyptianDate(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
        public static string ToEgyptianDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToEgyptianDate() : string.Empty;
        }

        public static bool IsVaildDate(this string date)
        {
            DateTime dt;
            return DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out dt);
        }

       

        public static DateTime FixDateToEgyptianDate(this DateTime date)
        {
            int day = date.Month;
            int Month = date.Day;
            int year = date.Year;
            return new DateTime(year, Month, day);
        }
    }
}
