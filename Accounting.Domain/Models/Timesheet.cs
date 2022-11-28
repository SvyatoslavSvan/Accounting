using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class Timesheet : EntityBase
    {
		public Timesheet()
		{

		}
		public Timesheet(ICollection<WorkDay> workDays, ICollection<BetEmployee> employees , HoursDaysInWorkMonth hoursDaysInWorkMonth, DateTime date)
		{
			WorkDays = workDays;
			HoursDaysInWorkMonth = hoursDaysInWorkMonth;
			Date = date;
			Employees = employees;
		}
		private ICollection<WorkDay> _workDays;

		public ICollection<WorkDay> WorkDays
		{
			get => _workDays;
			set 
			{ 
				_workDays = value ?? throw new ArgumentNullException(nameof(value)); 
			}
		}

		private HoursDaysInWorkMonth _hoursDaysInWorkMonth;

		public HoursDaysInWorkMonth HoursDaysInWorkMonth
        {
			get => _hoursDaysInWorkMonth;

			set 
			{
				_hoursDaysInWorkMonth = value ?? throw new ArgumentNullException(nameof(value)); 
			}
		}
		private ICollection<BetEmployee> _employees;

		public ICollection<BetEmployee> Employees
		{
			get => _employees;
			set { _employees = value; }
		}


		public DateTime Date { get; private set; }

	}
}
