using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Migrations;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;

namespace Accounting.DAL.Providers
{
    public class AccrualProvider : IAccrualProvider
    {
#nullable disable
        private readonly IAccrualRepository _accrualRepository;
        private readonly IUnitOfWork<ApplicationDBContext> _unitOfWork;
        private readonly ILogger<Accrual> _logger;
        public AccrualProvider(IAccrualRepository accrualRepository, IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<Accrual> logger)
        {
            _accrualRepository = accrualRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

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

        private BaseResult<T> HandleException<T>(Exception exception = null)
        {
            LogErrorMessage(exception);
            return new BaseResult<T>(false, default(T), OperationStatuses.Error);
        }

        private void LogErrorMessage(Exception ex = null)
        {
            var exception = ex ?? _unitOfWork.LastSaveChangesResult.Exception;
            _logger.LogError(exception.Message);
            _logger.LogError(exception.InnerException.Message ?? "");
            _logger.LogError(exception.StackTrace);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<Accrual>().Delete(await _unitOfWork.GetRepository<Accrual>().GetFirstOrDefaultAsync(predicate: x => x.Id == id)); // колхоз ибо в сборке ошибка
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

        public async Task<BaseResult<List<Accrual>>> GetAll()
        {
            try
            {
                var accruals = await _unitOfWork.GetRepository<Accrual>().GetAllAsync(disableTracking: false, include: x => x.Include(x => x.Employee));
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
