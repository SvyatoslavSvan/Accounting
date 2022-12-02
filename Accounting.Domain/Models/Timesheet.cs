using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class Timesheet : EntityBase
    {
		public Timesheet()
		{

		}

		public Timesheet(ICollection<WorkDay> workDays, ICollection<BetEmployee> employees , DateTime date, int daysCount, float hoursCount)
		{
			WorkDays = workDays;
			Date = date;
			Employees = employees;
			DaysCount = daysCount;
			HoursCount = hoursCount;
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

		private int _daysCount;

		public int DaysCount
		{
			get { return _daysCount; }
			set { _daysCount = value; }
		}

		private float _hourCount;

		public float HoursCount
		{
			get { return _hourCount; }
			set { _hourCount = value; }
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
