using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class WorkDay : EntityBase
    {
        public WorkDay() { }
        public WorkDay(DateTime date, float hours)
        {
            Date = date;
            Hours = hours;
        }
        public WorkDay(DateTime date, float hours, BetEmployee employee)
        {
            Date = date;
            Hours = hours;
            Employee = employee;
        }

        public DateTime Date { get; private set; }
        public const float MinHoursValue = 0;
        public const float MaxHoursValue = 8;
        private float _hours;
        public float Hours
        {
            get => _hours;
            set 
            { 
                if (value < MinHoursValue)
                {
                    _hours = MinHoursValue;
                    return;
                }
                if (value > MaxHoursValue)
                {
                    _hours = MaxHoursValue;
                    return;
                }
                _hours = value;
            }
        }

        public BetEmployee Employee { get; private set; } = null!;
        public Guid EmployeeId { get; private set; }
    }
}
