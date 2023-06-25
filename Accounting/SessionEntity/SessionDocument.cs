using Accounting.Domain.Models.Base;

namespace Accounting.SessionEntity
{
    public class SessionDocument
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Payout> Payouts { get; set; } = new List<Payout>();

        public void AddEmployee(Employee employeeBase)
        {
            Employees.Add(employeeBase);
        }

        public void AddPayout(Payout payout)
        {
            Payouts.Add(payout);
        }

        public void AddPayouts(IList<Payout> payouts)
        {
            Payouts.AddRange(payouts);
        }

        public List<Payout> GetPayoutsByEmployeeId(Guid employeeId) => Payouts.Where(x => x.EmployeeId == employeeId).ToList(); 

        public void UpdatePayout(Guid payoutId, decimal ammount)
        {
            Payouts.First(x => x.Id == payoutId).Ammount = ammount;
        }


        public void DeletePayout(Guid payoutId)
        {
            Payouts.RemoveAll(x => x.Id == payoutId);
        }

        public void DeleteEmployee(Guid employeeId, Guid payoutId)
        {
            Employees.RemoveAll(x => x.Id == employeeId);
            Payouts.RemoveAll(x => x.Id == payoutId);
        }
    }
}
