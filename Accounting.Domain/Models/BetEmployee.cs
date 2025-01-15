using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class BetEmployee : Employee
    {
#nullable disable
        [JsonConstructor]
        public BetEmployee(Guid id, string name) : base(id, name) { }

        public BetEmployee() { }

        public BetEmployee(string name, decimal bet, string innerId, int premium) : base(name, innerId, premium)
        {
            Bet = bet;
        }

        public BetEmployee(Guid id, Group group, string name, string innerId, decimal bet, int premium) : base(id, group, name, innerId, premium)
        {
            Bet = bet;
        }

        private decimal _bet;

        private List<WorkDay> _workDays;

        private ICollection<Timesheet> _timesheets;

        public decimal Bet
        {
            get => _bet;
            set 
            {
                const int minValue = 0;
                if (value < minValue)
                {
                    _bet = value;
                    return;
                }
                _bet = value; 
            }
        }

        [JsonIgnore]
        public List<WorkDay> WorkDays
        {
            get => _workDays;

            set 
            { 
                _workDays = value; 
            }
        }

        public ICollection<Timesheet> Timesheets
        {
            get => _timesheets;
            set { _timesheets = value; }
        }

        public override Salary CalculateSalary(DateTime from, DateTime to)
        {
            var salary = base.CalculateSalary(from, to);
            salary.Payment += CalculateBetPayout(from, to);
            return salary;
        }

        private decimal CalculateBetPayout(DateTime from, DateTime to)
        {
            decimal payment = 0;
            var timesheets = Timesheets.Where(x => x.Date.Year >= from.Date.Year && x.Date.Year <= to.Date.Year && x.Date.Month >= from.Date.Month && x.Date.Month <= to.Date.Month).ToList();
            timesheets.ForEach(x =>
            {
                var payForHour = (Bet / x.DaysCount) / (decimal)(x.HoursCount / x.DaysCount);
                var workHours = x.WorkDays.Where(x => x.EmployeeId == this.Id).Where(x => x.Date >= from.Date && x.Date <= to.Date).Sum(x => x.Hours);
                payment += (decimal)workHours * payForHour;
            });
            return payment; 
        }

       
    }
}
