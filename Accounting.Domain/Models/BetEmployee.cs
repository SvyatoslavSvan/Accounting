using Accounting.Domain.Models.Base;
using Accounting.Domain.ViewModels;

namespace Accounting.Domain.Models
{
    public class BetEmployee : EmployeeBase
    {
        public decimal Bet { get; private set; }

        private List<WorkDay> _workDays;

        public List<WorkDay> WorkDays
        {
            get => _workDays;

            set 
            { 
                _workDays = value; 
            }
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
