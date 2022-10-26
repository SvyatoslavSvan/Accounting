namespace Accounting.Domain.Models
{
    public class WorkDay
    {
        public WorkDay() { }
        public WorkDay(DateTime date, int hours)
        {
            Date = date;
            Hours = hours;
        }
        public WorkDay(DateTime date, int hours, BetEmployee employee)
        {
            Date = date;
            Hours = hours;
            Employee = employee;
        }

        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public int Hours { get; private set; }
        public BetEmployee Employee { get; private set; } = null!;
        public Guid EmployeeId { get; private set; }
    }
}
