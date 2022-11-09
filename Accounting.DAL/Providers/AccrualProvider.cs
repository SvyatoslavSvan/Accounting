using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Accounting.DAL.Providers
{
    public class AccrualProvider : ProviderBase , IAccrualProvider
    {
#nullable disable
       
        public AccrualProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<PayoutNotBetEmployee> logger) : base(unitOfWork, logger) { }
      
        public async Task<BaseResult<bool>> Create(PayoutNotBetEmployee entity)
        {
            _unitOfWork.DbContext.Attach(entity.Employee);
            await _unitOfWork.GetRepository<PayoutNotBetEmployee>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<PayoutNotBetEmployee>().Delete(id); 
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> DeleteRange(IList<PayoutNotBetEmployee> accruals)
        {
            _unitOfWork.GetRepository<PayoutNotBetEmployee>().Delete(accruals);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> DeleteRangeByDocumentId(Guid documentId)
        {
            var accrualsToDelete = await _unitOfWork.GetRepository<PayoutNotBetEmployee>().GetAllAsync(predicate: x => x.Document.Id == documentId);
            _unitOfWork.GetRepository<PayoutNotBetEmployee>().Delete(accrualsToDelete);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<PayoutNotBetEmployee>>> GetAll()
        {
            try
            {
                var accruals = await _unitOfWork.GetRepository<PayoutNotBetEmployee>().GetAllAsync(disableTracking: false, 
                    include: x => x.Include(x => x.Employee));
                return new BaseResult<List<PayoutNotBetEmployee>>(true, accruals.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<PayoutNotBetEmployee>>(ex);
            }
        }

        public async Task<BaseResult<IList<PayoutNotBetEmployee>>> GetAllByPredicate(Expression<Func<PayoutNotBetEmployee, bool>> predicate)
        {
            try
            {
                var accruals = await _unitOfWork.GetRepository<PayoutNotBetEmployee>().GetAllAsync(disableTracking: false,
                    include: x => x.Include(x => x.Employee),
                    predicate: predicate
                    );
                return new BaseResult<IList<PayoutNotBetEmployee>>(true, accruals.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<PayoutNotBetEmployee>>(ex);
            }
        }

        public async Task<BaseResult<PayoutNotBetEmployee>> GetById(Guid id)
        {
            try
            {
                return new BaseResult<PayoutNotBetEmployee>(true, await _unitOfWork.GetRepository<PayoutNotBetEmployee>().
                    GetFirstOrDefaultAsync(predicate: x => x.Id == id, include: x => x.Include(x => x.Employee), disableTracking: false), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<PayoutNotBetEmployee>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(PayoutNotBetEmployee entity)
        {
            _unitOfWork.GetRepository<PayoutNotBetEmployee>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
    }
}
