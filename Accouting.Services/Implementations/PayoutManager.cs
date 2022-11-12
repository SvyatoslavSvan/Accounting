using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accouting.Domain.Managers.Interfaces;

namespace Accouting.Domain.Managers.Implementations
{
    public class PayoutManager : IPayoutManager
    {
        private readonly IPayoutProvider _payoutProvider;
        public PayoutManager(IPayoutProvider payoutProvider) => _payoutProvider = payoutProvider;

        public async Task<BaseResult<PayoutBase>> Create(PayoutBase model)
        {
            var createResult = await _payoutProvider.Create(model);
            return new BaseResult<PayoutBase>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id) => await _payoutProvider.Delete(id);

        public Task<BaseResult<bool>> DeleteByDocumentId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<bool>> DeleteRange(List<PayoutNotBetEmployee> accruals)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult<IList<PayoutBase>>> GetAll()
        {
            var getAllResult = await _payoutProvider.GetAll();
            return new BaseResult<IList<PayoutBase>>(getAllResult.Succed,
                getAllResult.Data, getAllResult.OperationStatus);
        }

        public async Task<BaseResult<PayoutBase>> GetById(Guid id)
        {
            var getByIdResult = await _payoutProvider.GetById(id);
            return new BaseResult<PayoutBase>(getByIdResult.Succed, getByIdResult.Data, getByIdResult.OperationStatus);
        }

        public async Task<BaseResult<PayoutBase>> Update(PayoutBase model)
        {
            var updateResult = await _payoutProvider.Update(model);
            return new BaseResult<PayoutBase>(updateResult.Succed, model, updateResult.OperationStatus);
        }
    }
}
