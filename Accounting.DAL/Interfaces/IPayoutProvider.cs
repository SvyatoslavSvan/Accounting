using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IPayoutProvider : IBaseProvider<Payout>
    {
        public Task<BaseResult<bool>> DeleteRange(List<Payout> accruals);
        public Task<BaseResult<IList<Payout>>> GetAllByPredicate(Expression<Func<Payout, bool>> predicate);
    }
}
