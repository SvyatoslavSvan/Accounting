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
       
        public AccrualProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<Accrual> logger) : base(unitOfWork, logger) { }
      
        public async Task<BaseResult<bool>> Create(Accrual entity)
        {
            _unitOfWork.DbContext.Attach(entity.Employee);
            await _unitOfWork.GetRepository<Accrual>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<Accrual>().Delete(id); 
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> DeleteRange(List<Accrual> accruals)
        {
            _unitOfWork.GetRepository<Accrual>().Delete(accruals);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> DeleteRangeByDocumentId(Guid documentId)
        {
            var accrualsToDelete = await _unitOfWork.GetRepository<Accrual>().GetAllAsync(predicate: x => x.Document.Id == documentId);
            _unitOfWork.GetRepository<Accrual>().Delete(accrualsToDelete);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<Accrual>>> GetAll(Expression<Func<Accrual, bool>> predicate = null)
        {
            try
            {
                var accruals = await _unitOfWork.GetRepository<Accrual>().GetAllAsync(disableTracking: false, 
                    include: x => x.Include(x => x.Employee), predicate: predicate);
                return new BaseResult<List<Accrual>>(true, accruals.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<Accrual>>(ex);
            }
        }


        public async Task<BaseResult<Accrual>> GetById(Guid id)
        {
            try
            {
                return new BaseResult<Accrual>(true, await _unitOfWork.GetRepository<Accrual>().
                    GetFirstOrDefaultAsync(predicate: x => x.Id == id, include: x => x.Include(x => x.Employee), disableTracking: false), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<Accrual>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(Accrual entity)
        {
            _unitOfWork.GetRepository<Accrual>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
    }
}
