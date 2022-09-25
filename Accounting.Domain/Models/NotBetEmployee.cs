using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class NotBetEmployee : EmployeeBase
    {
        public List<Accrual> Accruals { get; private set; }
        public NotBetEmployee(string name, string innerId) : base(name, innerId)
        {
            
        }
    }
}
