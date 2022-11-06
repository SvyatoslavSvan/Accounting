using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IAccrualProvider : IBaseProvider<Accrual>
    {
        public Task<BaseResult<bool>> DeleteRange(IList<Accrual> accruals);
        public Task<BaseResult<bool>> DeleteRangeByDocumentId(Guid documentId);
        public Task<BaseResult<IList<Accrual>>> GetAllByPredicate(Expression<Func<Accrual, bool>> predicate);
    }
}
