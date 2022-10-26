using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;

namespace Accounting.DAL.Interfaces
{
    public interface IEmployeeProvider : IBaseProvider<EmployeeBase>
    {
        public Task<BaseResult<NotBetEmployee>> getNotBetEmployee(Guid id);
        public Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployees();
        public Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployeesIncludeDocument();
        public Task<BaseResult<BetEmployee>> GetBetEmployee(Guid id);
        public Task<BaseResult<IList<BetEmployee>>> GetBetEmployees();
    }
}
