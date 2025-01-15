using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models.Base;
using Accouting.Domain.Managers.Interfaces;

namespace Accouting.Domain.Managers.Implementations
{
    public class PayoutManager : IPayoutManager
    {
        private readonly IPayoutProvider _payoutProvider;
        public PayoutManager(IPayoutProvider payoutProvider) => _payoutProvider = payoutProvider;

        public async Task<BaseResult<Payout>> Create(Payout model)
        {
            var createResult = await _payoutProvider.Create(model);
            return new BaseResult<Payout>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id) => await _payoutProvider.Delete(id);

        public async Task<BaseResult<bool>> DeleteByDocumentId(Guid id)
        {
            var payouts = await _payoutProvider.GetAllByPredicate(x => x.Document.Id == id);
            return await _payoutProvider.DeleteRange(payouts.Data.ToList());
        }

        public Task<BaseResult<bool>> DeleteRange(List<Payout> payouts) => _payoutProvider.DeleteRange(payouts);

        public async Task<BaseResult<bool>> DeleteWithoutDocument()
        {
            var getPayoutsResult = await _payoutProvider.GetAllByPredicate(x => x.Document == null);
            if (getPayoutsResult.Succed) 
            {
                var deleteResult =  await _payoutProvider.DeleteRange(getPayoutsResult.Data.ToList());
                return deleteResult;
            }
            return new BaseResult<bool>(getPayoutsResult.Succed, getPayoutsResult.Succed);
        }

        public async Task<BaseResult<IList<Payout>>> GetAll()
        {
            var getAllResult = await _payoutProvider.GetAll();
            return new BaseResult<IList<Payout>>(getAllResult.Succed,
                getAllResult.Data, getAllResult.OperationStatus);
        }

        public async Task<BaseResult<Payout>> GetById(Guid id)
        {
            var getByIdResult = await _payoutProvider.GetById(id);
            return new BaseResult<Payout>(getByIdResult.Succed, getByIdResult.Data, getByIdResult.OperationStatus);
        }

        public async Task<BaseResult<Payout>> Update(Payout model)
        {
            var updateResult = await _payoutProvider.Update(model);
            return new BaseResult<Payout>(updateResult.Succed, model, updateResult.OperationStatus);
        }
    }
}
