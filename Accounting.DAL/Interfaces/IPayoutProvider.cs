using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IPayoutProvider : IBaseProvider<PayoutBase>
    {
        public Task<BaseResult<bool>> DeleteRange(List<PayoutBase> accruals);
        public Task<BaseResult<IList<PayoutBase>>> GetAllByPredicate(Expression<Func<PayoutBetEmployee, 
            bool>> predicatePayoutBetEmployee, Expression<Func<PayoutNotBetEmployee, bool>> predicatePayoutNotBetEmployee);
    }
}
