using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class BetEmployee : EmployeeBase
    {
        public decimal Bet { get; private set; }
        public BetEmployee(string name, decimal bet, string innerId) : base(name, innerId)
        {
            Bet = bet;
        }

        public override decimal CalculateSalary(DateTime from)
        {
            throw new NotImplementedException();
        }
    }
}
