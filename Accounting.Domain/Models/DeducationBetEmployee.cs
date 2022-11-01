using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class DeducationBetEmployee : DeducationBase
    {
        public override void ToSerializable()
        {
            base.ToSerializable();
            Employee = null;
        }
        public DeducationBetEmployee() {}
        public DeducationBetEmployee(decimal ammount,bool isAdditional, BetEmployee employee) : base(ammount, isAdditional) => Employee = employee;        
        public BetEmployee Employee { get; private set; }
        public Guid EmployeeId { get; private set; }
    }
}
