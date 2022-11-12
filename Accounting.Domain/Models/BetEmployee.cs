using Accounting.Domain.Models.Base;
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
        
        public override Salary CalculateSalary()
        {
            throw new NotImplementedException();
        }

        
    }
}
