using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IWorkDayProvider : IBaseProvider<WorkDay>
    {
        public Task<int> Count(Expression<Func<WorkDay, bool>> predicate);
        public Task<BaseResult<bool>> CreateRange(IList<WorkDay> workDays);
        public Task<BaseResult<IList<WorkDay>>> GetAllByPredicate(Expression<Func<WorkDay, bool>> predicate);
    }
}
