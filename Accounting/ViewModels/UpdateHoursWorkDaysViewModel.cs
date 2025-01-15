namespace Accounting.ViewModels
{
    public class UpdateHoursWorkDaysViewModel
    {
        public Guid TimesheetId { get; set; }
        public int WorkDays { get; set; }
        public float WorkHours { get; set; }
    }
}
