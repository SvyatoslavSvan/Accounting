using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;

namespace Accounting.Services.Interfaces
{
    public interface ISessionDeducationDocumentService
    {
        public Task<bool> AddEmployee(EmployeeBase employee);
        public Task<bool> DeleteEmployee(EmployeeBase employee);
        public Task<bool> CreateSessionDeducationDocument();
        public EmployeeBase GetEmployeeById(Guid id);
        public List<DeducationBase> GetDeducationsByEmployeeId(Guid employeeId);
    }
}
