using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;

namespace Accounting.Domain.SessionEntity
{
    public class SessionDeducationDocument
    {

        public List<DeducationBetEmployee> DeducationBetEmployees { get; set; } = new List<DeducationBetEmployee>();
        public List<DeducationNotBetEmployee> DeducationNotBetEmployees { get; set; } = new List<DeducationNotBetEmployee>();
        public List<BetEmployee> BetEmployees { get; set; } = new List<BetEmployee>();
        public List<NotBetEmployee> NotBetEmployees { get; set; } = new List<NotBetEmployee>(); 
        public EmployeeBase GetEmployee(Guid id)
        {
            EmployeeBase employee;
            employee = BetEmployees.FirstOrDefault(x => x.Id == id);
            if (employee is null)
            {
                employee = NotBetEmployees.FirstOrDefault(x => x.Id == id);
            }
            return employee;
        }
        public List<DeducationBase> getDeducationsByEmployeeId(Guid id)
        {
            var deducations = new List<DeducationBase>();
            deducations.AddRange(DeducationNotBetEmployees.Where(x => x.EmployeeId == id));
            deducations.AddRange(DeducationBetEmployees.Where(x => x.EmployeeId == id));
            return deducations;
        }
        public void RemoveEmployee(EmployeeBase employee)
        {
            if (employee is BetEmployee)
            {
                BetEmployees.RemoveAll(x => x.Id == employee.Id);
                return;
            }
            if (employee is NotBetEmployee)
            {
                NotBetEmployees.RemoveAll(x => x.Id == employee.Id);
            }
        }
    }
}
