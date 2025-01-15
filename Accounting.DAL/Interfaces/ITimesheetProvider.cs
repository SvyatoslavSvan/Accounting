using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface ITimesheetProvider : IBaseProvider<Timesheet>
    {
        public Task<BaseResult<int>> Count(Expression<Func<Timesheet, bool>> expression);
        public Task<BaseResult<Timesheet>> GetByPredicate(Expression<Func<Timesheet, bool>> predicate, Func<IQueryable<Timesheet>, IIncludableQueryable<Timesheet, object>>? include = null);
    }
}
