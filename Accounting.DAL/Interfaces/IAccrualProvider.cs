using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IAccrualProvider : IBaseProvider<PayoutNotBetEmployee>
    {
        public Task<BaseResult<bool>> DeleteRange(IList<PayoutNotBetEmployee> accruals);
        public Task<BaseResult<bool>> DeleteRangeByDocumentId(Guid documentId);
        public Task<BaseResult<IList<PayoutNotBetEmployee>>> GetAllByPredicate(Expression<Func<PayoutNotBetEmployee, bool>> predicate);
    }
}
