using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Accounting.DAL.Providers
{
    public class TimesheetProvider : ProviderBase, ITimesheetProvider
    {
        public TimesheetProvider(IUnitOfWork<ApplicationDBContext> dbContext, ILogger<Timesheet> logger) : base(dbContext, logger) { }

        public async Task<BaseResult<int>> Count(Expression<Func<Timesheet, bool>> expression)
        {
            try
            {
                var count = await _unitOfWork.GetRepository<Timesheet>().CountAsync(expression);
                return new BaseResult<int>(true, count, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<int>(ex);
            }
        }

        public async Task<BaseResult<bool>> Create(Timesheet entity)
        {
            _unitOfWork.DbContext.AttachRange(entity.Employees);
            _unitOfWork.DbContext.AttachRange(entity.WorkDays);
            await _unitOfWork.GetRepository<Timesheet>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<Timesheet>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<Timesheet>>> GetAll()
        {
            try
            {
                var timesheets = await _unitOfWork.GetRepository<Timesheet>().GetAllAsync(disableTracking: true);
                return new BaseResult<List<Timesheet>>(true, timesheets.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<Timesheet>>(ex);
            }
        }

       

        public async Task<BaseResult<Timesheet>> GetById(Guid id)
        {
            try
            {
                return new BaseResult<Timesheet>(true, await _unitOfWork.GetRepository<Timesheet>().GetFirstOrDefaultAsync(predicate: x => x.Id == id), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<Timesheet>(ex);
            }
        }

        public async Task<BaseResult<Timesheet>> GetByPredicate(Expression<Func<Timesheet, bool>> predicate, Func<IQueryable<Timesheet>, IIncludableQueryable<Timesheet, object>>? include = null)
        {
            try
            {
                return new BaseResult<Timesheet>(true, await _unitOfWork.GetRepository<Timesheet>().GetFirstOrDefaultAsync(predicate: predicate, include: include), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<Timesheet>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(Timesheet entity)
        {
            _unitOfWork.GetRepository<Timesheet>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
    }
}
