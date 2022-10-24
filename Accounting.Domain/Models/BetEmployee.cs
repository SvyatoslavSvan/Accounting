using Accounting.Domain.Models.Base;
using Accounting.Domain.ViewModels;

namespace Accounting.Domain.Models
{
    public class BetEmployee : EmployeeBase
    {
        public decimal Bet { get; private set; }
        public BetEmployee(string name, decimal bet, string innerId) : base(name, innerId)
        {
            Bet = bet;
        }

        public BetEmployee(Guid id, Group group, string name, string innerId, decimal bet) : base(id, group, name, innerId)
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
