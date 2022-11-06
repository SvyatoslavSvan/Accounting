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

        public List<WorkDay> WorkDays
        {
            get => _workDays;

            set 
            { 
                _workDays = value; 
            }
        }
        private ICollection<DeducationBetEmployee> _deducationBetEmployee;

        public ICollection<DeducationBetEmployee> DeducationBetEmployee
        {
            get { return _deducationBetEmployee; }
            set { _deducationBetEmployee = value; }
        }

        public override void ToSerializable()
        {
            base.ToSerializable();
            this.WorkDays = null;
        }

        public BetEmployee(string name, decimal bet, string innerId, int premium) : base(name, innerId, premium)
        {
            Bet = bet;
        }

        public BetEmployee(Guid id, Group group, string name, string innerId, decimal bet, int premium) : base(id, group, name, innerId, premium)
        {
            Bet = bet;
        }
        
        public override decimal CalculateSalary(DateTime from)
        {
            throw new NotImplementedException();
        }

        
    }
}
