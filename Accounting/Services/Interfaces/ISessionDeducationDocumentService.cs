using Accounting.Domain.Models.Base;
using Accounting.Domain.SessionEntity;
using Accounting.Domain.ViewModels;

namespace Accounting.Services.Interfaces
{
    public interface ISessionDeducationDocumentService
    {
        public Task<bool> AddEmployee(EmployeeBase employee);
        public Task<bool> DeleteEmployee(EmployeeBase employee);
        public Task<bool> CreateSessionDeducationDocument();
        public EmployeeBase GetEmployeeById(Guid id);
        public List<DeducationBase> GetDeducationsByEmployeeId(Guid employeeId);
        public Task DeleteDeducation(Guid deducationId);
        public Task<bool> UpdateDeducation(UpdateDeducationlViewModel viewModel);
        public Task<bool> AddDeducation(DeducationBase deducation);
        public decimal GetSum();
        public SessionDeducationDocument GetDocumentFromSession();
    }
}
