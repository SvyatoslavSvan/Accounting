using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IEmployeeProvider : IBaseProvider<Employee>
    {
        public Task<BaseResult<Employee>> GetNotBetEmployee(Guid id);
        public Task<BaseResult<IEnumerable<Employee>>> GetNotBetEmployees();
        public Task<BaseResult<IEnumerable<Employee>>> GetNotBetEmployeesIncludeDocument();
        public Task<BaseResult<BetEmployee>> GetBetEmployee(Guid id);
        public Task<BaseResult<IList<BetEmployee>>> GetBetEmployees();
        public Task<BaseResult<IList<Employee>>> GetAllByPredicate(
            Expression<Func<BetEmployee, bool>> betEmployeePredicate = null, 
            Expression<Func<Employee, bool>> notBetEmployeePredicate = null,
            Func<IQueryable<BetEmployee>, IIncludableQueryable<BetEmployee, object>>? includeBetEmployee = null, 
            Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>>? includeNotBetEmployee = null, Func<IQueryable<BetEmployee>, IOrderedQueryable<BetEmployee>>? orderByBetEmployee = null,
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? orderByNotBetEmployee = null);
        public Task<BaseResult<IList<BetEmployee>>> GetBetEmployeesWithInclude(Func<IQueryable<BetEmployee>, IIncludableQueryable<BetEmployee, object>>? include);
        
    }
}
