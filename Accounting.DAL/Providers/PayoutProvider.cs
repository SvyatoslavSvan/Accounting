using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Accounting.DAL.Providers
{
    public class PayoutProvider : ProviderBase, IPayoutProvider
    {
#nullable disable
        public PayoutProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<Payout> logger) : base(unitOfWork, logger) { }

        public async Task<BaseResult<bool>> Create(Payout entity)
        {
            _unitOfWork.DbContext.Attach(entity.Employee);
            await _unitOfWork.GetRepository<Payout>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<Payout>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> DeleteRange(List<Payout> payouts)
        {
            _unitOfWork.GetRepository<Payout>().Delete(payouts);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<Payout>>> GetAll()
        {
            try
            {
                var payouts = await _unitOfWork.GetRepository<Payout>().GetAllAsync(disableTracking: true);
                return new BaseResult<List<Payout>>(true, payouts.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<Payout>>(ex);
            }
        }

        public async Task<BaseResult<IList<Payout>>> GetAllByPredicate(Expression<Func<Payout, bool>> predicate)
        {
            try
            {
                var payouts = new List<Payout>();
                payouts.AddRange(await _unitOfWork.GetRepository<Payout>().GetAllAsync(predicate: predicate));
                return new BaseResult<IList<Payout>>(true, payouts, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<Payout>>(ex);
            }
        }

        public async Task<BaseResult<Payout>> GetById(Guid id)
        {
            try
            {
                    return new BaseResult<Payout>(true, await _unitOfWork.GetRepository<Payout>().GetFirstOrDefaultAsync(predicate: x => x.Id == id), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<Payout>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(Payout entity)
        {
            _unitOfWork.GetRepository<Payout>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
    }
}
