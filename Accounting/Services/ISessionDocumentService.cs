using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.SessionEntity;

namespace Accounting.Services
{
    public interface ISessionDocumentService
    {
        public Task<bool> Create();
        public Task<bool> AddEmployeeToDocument(Employee employee);
        public List<Payout> GetPayoutsByEmployeeId(Guid employeeId);
        public Task<bool> AddPayout(Payout payout);
        public Task<bool> UpdatePayout(Guid payoutId, decimal ammount);
        public Task<bool> DeleteAccrual(Guid payoutId);
        public Task<bool> DeleteEmployee(Guid employeeId, Guid PayoutId);
        public SessionDocument GetDocument();
        public Task<bool> LoadDocument(Document document);
        public decimal GetSumOfPayouts();
        public int GetCountOfTwinsEmployees(Guid id);
        public IList<Payout> GetPayouts();
        public IList<Employee> GetEmployees();
    }
}
