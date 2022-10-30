using Accounting.Domain.Models.Base;
using Accounting.Domain.ViewModels;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class BetEmployee : EmployeeBase
    {
        [JsonConstructor]
        public BetEmployee(Guid id, string name) : base(id, name) { }
        public decimal Bet { get; private set; }
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

        public void Update(UpdateEmployeeViewModel betEmployee, Group group)
        {
            Bet = (decimal)betEmployee.Bet;
            Name = betEmployee.Name; 
            InnerId = betEmployee.InnerId;
            Group = group;
        }
    }
}
