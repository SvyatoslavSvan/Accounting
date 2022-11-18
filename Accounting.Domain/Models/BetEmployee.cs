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
            decimal payout = 0;
            var monthBetween = to.Month - from.Month;
            var monthToCalculate = from.Month;
            for (int i = 0; i <= monthBetween; i++)
            {
                var payForDay = Bet / WorkDays.Where(x => x.Date.Month == monthToCalculate && x.Hours != 0).Count();
                var payForHour =  payForDay / 8;
                var workDays = WorkDays.Where(x => x.Date.Date >= from.Date && x.Date.Date <= to.Date && x.Date.Month == monthToCalculate).ToList();
                workDays.ForEach(x =>
                {
                    payout += (decimal)x.Hours * payForHour;
                });
                monthToCalculate += 1;
            }
            return payout;
        }

    }
}
