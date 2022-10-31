using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class DeducationBetEmployee : DeducationBase
    {
        public DeducationBetEmployee() {}
        public DeducationBetEmployee(decimal ammount,bool isAdditional, BetEmployee employee) : base(ammount, isAdditional) => Employee = employee;        
        public BetEmployee Employee { get; private set; }
        public Guid EmployeeId { get; private set; }
    }
}
