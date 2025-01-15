namespace Accounting.ViewModels
{
    public class UpdateWorkDaysForEmployeeViewModel
    {
        public Guid EmployeeId { get; set; }
        public float Value { get; set; }
        public Guid TimesheetId { get; set; }
    }
}
