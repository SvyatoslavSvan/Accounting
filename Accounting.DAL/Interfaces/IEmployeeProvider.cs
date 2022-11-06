﻿using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IEmployeeProvider : IBaseProvider<EmployeeBase>
    {
        public Task<BaseResult<NotBetEmployee>> GetNotBetEmployee(Guid id);
        public Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployees();
        public Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployeesIncludeDocument();
        public Task<BaseResult<BetEmployee>> GetBetEmployee(Guid id);
        public Task<BaseResult<IList<BetEmployee>>> GetBetEmployees();
        public Task<BaseResult<IList<EmployeeBase>>> GetAllByPredicate(Expression<Func<bool, BetEmployee>> betEmployeePredicate, Expression<Func<bool, NotBetEmployee>> notBetEmployeePredicate);
        public Task<BaseResult<IList<BetEmployee>>> GetBetEmployeesWithInclude(Func<IQueryable<BetEmployee>, IIncludableQueryable<BetEmployee, object>>? include);
        
    }
}
