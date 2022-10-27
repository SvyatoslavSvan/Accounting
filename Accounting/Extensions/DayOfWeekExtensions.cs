
namespace Accounting.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static string ToUkranianShortcutDayOfWeek(this DayOfWeek dayOfWeek, DateTime day)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday: 
                    return "Нд" + day.Day;
                case DayOfWeek.Monday: 
                    return "Пн" + day.Day;
                case DayOfWeek.Tuesday:
                    return "Вт" + day.Day;
                case DayOfWeek.Wednesday:
                    return "Ср" + day.Day;
                case DayOfWeek.Thursday:
                    return "Чт" + day.Day;
                case DayOfWeek.Friday:
                    return "Пт" + day.Day;
                case DayOfWeek.Saturday:
                    return "Сб" + day.Day;
                default:
                    return string.Empty;
            }
        }
    }
}
