using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class HoursDaysInWorkMonth : EntityBase
    {
        public HoursDaysInWorkMonth(float hoursCount, Timesheet timesheet, int daysCount)
        {
            HoursCount = hoursCount;
            Timesheet = timesheet;
            DaysCount = daysCount;
        }

        public HoursDaysInWorkMonth(float hoursCount, int daysCount)
        {
            HoursCount = hoursCount;
            DaysCount = daysCount;
        }

        public float HoursCount { get; set; }
        public Timesheet Timesheet { get; private set; } = null!;
        public Guid TimesheetId { get; private set; }
        public int DaysCount { get; set; }
    }
}
