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
        //public int Hours { get; private set; }
        private int _hours;
        public int Hours
        {
            get => _hours;
            set 
            {
                const int minValue = 0;
                const int maxValue = 8;
                if (value < minValue)
                {
                    _hours = minValue;
                    return;
                }
                if (value > maxValue)
                {
                    _hours = maxValue;
                    return;
                }
                _hours = value;
            }
        }

        public BetEmployee Employee { get; private set; } = null!;
        public Guid EmployeeId { get; private set; }
    }
}
