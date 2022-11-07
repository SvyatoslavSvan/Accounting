using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class AccrualNotBetEmployee : AccrualBase
    {
#nullable disable
        public AccrualNotBetEmployee() { }
        public AccrualNotBetEmployee(decimal ammount, bool isAdditional) : base(ammount, isAdditional) { } 
        
        public AccrualNotBetEmployee(decimal ammount, Guid id, bool isAdditional) : base(ammount, id, isAdditional) { }

        public Guid EmployeeId { get; private set; }
        private NotBetEmployee _employee = null!;   

        public NotBetEmployee Employee
        {
            get => _employee;
            set { _employee = value ?? throw new ArgumentNullException(); }
        }


    }
}
