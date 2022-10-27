using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Accounting.DAL.Providers
{
    public class WorkDayProvider : IWorkDayProvider
    {
#nullable disable
        private readonly IUnitOfWork<ApplicationDBContext> _unitOfWork;
        private readonly ILogger<WorkDay> _logger;
        public WorkDayProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<WorkDay> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Task<BaseResult<bool>> Create(WorkDay entity)
        {
            throw new NotImplementedException();
        }

        private List<WorkDay> GetWorkDays(BetEmployee employee)
        {
            const float dontWork = WorkDay.MinHoursValue;
            const float workHours = WorkDay.MaxHoursValue;
            var workDays = new List<WorkDay>();
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                var day = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                {
                    workDays.Add(new WorkDay(day, dontWork, employee));
                }
                else
                {
                    workDays.Add(new WorkDay(day, workHours, employee));
                }
            }
            return workDays;
        }

        private IList<WorkDay> GetWorkDaysWithEmployees(IList<BetEmployee> employees)
        {
            var workDays = new List<WorkDay>();
            foreach (var item in employees)
            {
                workDays.AddRange(GetWorkDays(item));
            }
            return workDays;
        }
        private async Task<bool> IsNeededToCreateNewDays() => await _unitOfWork.GetRepository<WorkDay>().CountAsync(predicate: x => x.Date.Month == DateTime.Now.Month) == 0 ? true : false;
        public async Task<BaseResult<bool>> CreateNewWorkDays()
        {
            if (await IsNeededToCreateNewDays())
            {
                var employees = await _unitOfWork.GetRepository<BetEmployee>().GetAllAsync(true);
                var workDays = GetWorkDaysWithEmployees(employees);
                _unitOfWork.DbContext.AttachRange(employees);
                await _unitOfWork.GetRepository<WorkDay>().InsertAsync(workDays);
                await _unitOfWork.SaveChangesAsync();
                if (!_unitOfWork.LastSaveChangesResult.IsOk)
                {
                    return HandleException<bool>();
                }
                return new BaseResult<bool>(true, true, OperationStatuses.Ok);
            }
            else
            {
                return new BaseResult<bool>(true , true, OperationStatuses.AllWorkDaysMatch);
            }
        }

        public Task<BaseResult<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        private void LogErrorMessage(Exception ex = null)
        {
            var exception = ex ?? _unitOfWork.LastSaveChangesResult.Exception;
            _logger.LogError(exception.Message);
            _logger.LogError(exception.InnerException.Message ?? string.Empty);
            _logger.LogError(exception.StackTrace);
        }
        public BaseResult<T> HandleException<T>(Exception exception = null)
        {
            LogErrorMessage(exception);
            return new BaseResult<T>(false, default(T), OperationStatuses.Error);
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

        public async Task<BaseResult<IList<WorkDay>>> GetAllFromThisMonth()
        {
            try
            {
                return new BaseResult<IList<WorkDay>>(true, await 
                    _unitOfWork.GetRepository<WorkDay>().GetAllAsync(predicate: x => x.Date.Month == DateTime.Now.Month, include: x => x.Include(x => x.Employee)), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<WorkDay>>(ex);
            }
        }
    }
}
