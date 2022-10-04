using Accounting.Domain.Models;
using Accounting.Domain.SessionEntity;
using Accounting.Domain.ViewModels;

namespace Accounting.Services.Interfaces
{
    public interface ISessionDocumentService
    {
        public Task<bool> AddEmployee(NotBetEmployee employee);
        public Document GetDocument(DocumentViewModel documentViewModel);
        public Task<bool> Clear();
        public Task<bool> CreateSessionDocument();
        public Task<bool> AddAccrual(Accrual accrual);
        public int SumOfAccruals();
        public Task<bool> DeleteEmployee(Guid Id);
        public Task<bool> DeleteAccrual(Guid Id);
    }
}
