using Accounting.Domain.Models.Base;

namespace Accounting.Services
{
    public interface ISessionDocumentService
    {
        public Task<bool> Create();
        public Task<bool> AddEmployeeToDocument(EmployeeBase employee);
        public List<PayoutBase> GetAccrualsByEmployeeId(Guid employeeId);
        public Task<bool> AddPayout(PayoutBase payout);
        public Task<bool> UpdatePayout(Guid payoutId, decimal ammount);
        public Task<bool> DeleteAccrual(Guid payoutId);
    }
}
