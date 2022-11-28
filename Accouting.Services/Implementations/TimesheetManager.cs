using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces;
using Accounting.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accouting.Domain.Managers.Implementations
{
    public class TimesheetManager : ITimesheetManager
    {
        private readonly ITimesheetProvider _provider;
        private readonly IWorkDayManager _workDayManager;

        public TimesheetManager(ITimesheetProvider provider, IWorkDayManager workDayManager)
        {
            _provider = provider;
            _workDayManager = workDayManager;
        }

        public async Task<BaseResult<Timesheet>> Create(Timesheet model)
        {
            var createResult = await _provider.Create(model);
            return new BaseResult<Timesheet>(createResult.Succed, model, createResult.OperationStatus);
        }

        private async Task<bool> IsNeedToCreateTimesheet()
        {
            var countResult = await _provider.Count(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month);
            if (countResult.Succed)
            {
                return countResult.Data == 0 ? true : false;
            }
            return false;
        }

        public async Task<BaseResult<bool>> Delete(Guid id) => await _provider.Delete(id);  

        public async Task<BaseResult<IList<Timesheet>>> GetAll()
        {
            var timesheets = await _provider.GetAll();
            return new BaseResult<IList<Timesheet>>(timesheets.Succed, timesheets.Data, timesheets.OperationStatus);
        }

        public async Task<BaseResult<Timesheet>> GetById(Guid id) => await _provider.GetById(id);
       

        public async Task<BaseResult<Timesheet>> Update(Timesheet model)
        {
            var updateResult = await _provider.Update(model);
            return new BaseResult<Timesheet>(updateResult.Succed, model, updateResult.OperationStatus);
        }


        public async Task<BaseResult<Timesheet>> CreateNew()
        {
            if (await IsNeedToCreateTimesheet())
            {
                var createWorkDaysResult = await _workDayManager.CreateNewWorkDays();
                if (createWorkDaysResult.Succed)
                {
                    var timesheet = new Timesheet(createWorkDaysResult.Data, createWorkDaysResult.Employees, new HoursDaysInWorkMonth((int)createWorkDaysResult.Data.Sum(x => x.Hours), createWorkDaysResult.Data.Count()), DateTime.Now.Date);
                    var createResult = await _provider.Create(timesheet);
                    return new BaseResult<Timesheet>(createResult.Succed, timesheet, OperationStatuses.Ok);
                }
            }
            return new BaseResult<Timesheet>(true, null, OperationStatuses.AllTimesheetsMatch);
        }

        public async Task<BaseResult<Timesheet>> GetByDate(DateTime date) => await _provider.GetByPredicate(x => x.Date.Year == date.Year && x.Date.Month == date.Month, x => x.Include(x => x.Employees).ThenInclude(x => x.WorkDays));
    }
}
