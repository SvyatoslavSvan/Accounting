using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces;
using Accounting.DAL.Interfaces;
using System.Net.Http.Headers;

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
            throw new NotImplementedException();
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

        public Task<BaseResult<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<IList<Timesheet>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<Timesheet>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<Timesheet>> Update(Timesheet model)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult<Timesheet>> CreateNew()
        {
            if (await IsNeedToCreateTimesheet())
            {
                var createWorkDaysResult = await _workDayManager.CreateNewWorkDays();
                if (createWorkDaysResult.Succed)
                {
                    var timesheet = new Timesheet(createWorkDaysResult.Data, new HoursDaysInWorkMonth((int)createWorkDaysResult.Data.Sum(x => x.Hours), createWorkDaysResult.Data.Count()), DateTime.Now.Date);
                    var createResult = await _provider.Create(timesheet);
                    return new BaseResult<Timesheet>(createResult.Succed, timesheet, OperationStatuses.Ok);
                }
            }
            return new BaseResult<Timesheet>(true, null, OperationStatuses.AllTimesheetsMatch);
        }
    }
}
