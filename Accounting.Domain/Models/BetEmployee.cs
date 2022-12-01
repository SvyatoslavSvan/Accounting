using Accounting.Domain.Models.Base;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class BetEmployee : EmployeeBase
    {
        [JsonConstructor]
        public BetEmployee(Guid id, string name) : base(id, name) { }
        public BetEmployee() { }

        private decimal _bet;

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
#nullable disable

        private List<WorkDay> _workDays;
        [JsonIgnore]
        public List<WorkDay> WorkDays
        {
            get => _workDays;

            set 
            { 
                _workDays = value; 
            }
        }
        private ICollection<Timesheet> _timesheets;

        public ICollection<Timesheet> Timesheets
        {
            get => _timesheets;
            set { _timesheets = value; }
        }


        private ICollection<PayoutBetEmployee> _accruals;
        [JsonIgnore]
        public ICollection<PayoutBetEmployee> Accruals
        {
            get { return _accruals; }
            set { _accruals = value ?? throw new ArgumentNullException(); }
        }

        public BetEmployee(string name, decimal bet, string innerId, int premium) : base(name, innerId, premium)
        {
            Bet = bet;
        }

        public BetEmployee(Guid id, Group group, string name, string innerId, decimal bet, int premium) : base(id, group, name, innerId, premium)
        {
            Bet = bet;
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
            var timesheets = Timesheets.Where(x =>x.Date.Month >= from.Date.Month && x.Date.Year >= from.Date.Year && x.Date.Month <= to.Month && x.Date.Year <= to.Date.Year).ToList();
            timesheets.ForEach(x =>
            {
                var payForHour = (Bet / x.HoursDaysInWorkMonth.DaysCount) / (decimal)(x.HoursDaysInWorkMonth.HoursCount / x.HoursDaysInWorkMonth.DaysCount);
                var workDays = x.WorkDays.Where(x => x.EmployeeId == Id).Where(x => x.Date >= from.Date && x.Date <= to.Date);
                var workHours = workDays.Sum(x => x.Hours);
                payment += (decimal)workHours * payForHour;
            });
            return payment; 
        }

       
    }
}
