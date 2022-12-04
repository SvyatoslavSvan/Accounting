using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Accounting.DAL.Providers
{
    public class WorkDayProvider : ProviderBase , IWorkDayProvider
    {
#nullable disable

        public WorkDayProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<WorkDay> logger) : base(unitOfWork, logger) { }
        

        public Task<BaseResult<bool>> Create(WorkDay entity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<List<WorkDay>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult<WorkDay>> GetById(Guid id)
        {
            try
            {
                var workDay = await _unitOfWork.GetRepository<WorkDay>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
                return new BaseResult<WorkDay>(true, workDay, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<WorkDay>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(WorkDay entity)
        {
            _unitOfWork.GetRepository<WorkDay>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public Task<int> Count(Expression<Func<WorkDay, bool>> predicate) => _unitOfWork.GetRepository<WorkDay>().CountAsync(predicate: predicate);

        public async Task<BaseResult<bool>> CreateRange(IList<WorkDay> workDays)
        {
            foreach (var item in workDays)
            {
                _unitOfWork.DbContext.Attach(item.Employee);
            }
            await _unitOfWork.GetRepository<WorkDay>().InsertAsync(workDays);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<IList<WorkDay>>> GetAllByPredicate(Expression<Func<WorkDay, bool>> predicate)
        {
            try
            {
                return new BaseResult<IList<WorkDay>>(true, await
                    _unitOfWork.GetRepository<WorkDay>().GetAllAsync(predicate: predicate), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<WorkDay>>(ex);
            }
        }

        public async Task<BaseResult<bool>> UpdateRange(IList<WorkDay> workDays)
        {
            foreach (var item in workDays)
            {
                _unitOfWork.DbContext.Attach(item.Timesheet);
            }
            _unitOfWork.GetRepository<WorkDay>().Update(workDays);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
    }
}
