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
       
        public AccrualProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<AccrualNotBetEmployee> logger) : base(unitOfWork, logger) { }
      
        public async Task<BaseResult<bool>> Create(AccrualNotBetEmployee entity)
        {
            _unitOfWork.DbContext.Attach(entity.Employee);
            await _unitOfWork.GetRepository<AccrualNotBetEmployee>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<AccrualNotBetEmployee>().Delete(id); 
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> DeleteRange(IList<AccrualNotBetEmployee> accruals)
        {
            _unitOfWork.GetRepository<AccrualNotBetEmployee>().Delete(accruals);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> DeleteRangeByDocumentId(Guid documentId)
        {
            var accrualsToDelete = await _unitOfWork.GetRepository<AccrualNotBetEmployee>().GetAllAsync(predicate: x => x.Document.Id == documentId);
            _unitOfWork.GetRepository<AccrualNotBetEmployee>().Delete(accrualsToDelete);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<AccrualNotBetEmployee>>> GetAll()
        {
            try
            {
                var accruals = await _unitOfWork.GetRepository<AccrualNotBetEmployee>().GetAllAsync(disableTracking: false, 
                    include: x => x.Include(x => x.Employee));
                return new BaseResult<List<AccrualNotBetEmployee>>(true, accruals.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<AccrualNotBetEmployee>>(ex);
            }
        }

        public async Task<BaseResult<IList<AccrualNotBetEmployee>>> GetAllByPredicate(Expression<Func<AccrualNotBetEmployee, bool>> predicate)
        {
            try
            {
                var accruals = await _unitOfWork.GetRepository<AccrualNotBetEmployee>().GetAllAsync(disableTracking: false,
                    include: x => x.Include(x => x.Employee),
                    predicate: predicate
                    );
                return new BaseResult<IList<AccrualNotBetEmployee>>(true, accruals.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<AccrualNotBetEmployee>>(ex);
            }
        }

        public async Task<BaseResult<AccrualNotBetEmployee>> GetById(Guid id)
        {
            try
            {
                return new BaseResult<AccrualNotBetEmployee>(true, await _unitOfWork.GetRepository<AccrualNotBetEmployee>().
                    GetFirstOrDefaultAsync(predicate: x => x.Id == id, include: x => x.Include(x => x.Employee), disableTracking: false), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<AccrualNotBetEmployee>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(AccrualNotBetEmployee entity)
        {
            _unitOfWork.GetRepository<AccrualNotBetEmployee>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
    }
}
