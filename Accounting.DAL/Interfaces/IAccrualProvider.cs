using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IAccrualProvider : IBaseProvider<AccrualNotBetEmployee>
    {
        public Task<BaseResult<bool>> DeleteRange(IList<AccrualNotBetEmployee> accruals);
        public Task<BaseResult<bool>> DeleteRangeByDocumentId(Guid documentId);
        public Task<BaseResult<IList<AccrualNotBetEmployee>>> GetAllByPredicate(Expression<Func<AccrualNotBetEmployee, bool>> predicate);
    }
}
