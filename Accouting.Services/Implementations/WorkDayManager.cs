using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces;
using Accouting.Domain.Managers.Result;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Accouting.Domain.Managers.Implementations
{
    public class WorkDayManager : IWorkDayManager
    {
        private readonly IWorkDayProvider _workDayProvider;
        private readonly IEmployeeManager _employeeManager;

        public WorkDayManager(IWorkDayProvider workDayProvider, IEmployeeManager employeeManager)
        {
            _workDayProvider = workDayProvider;
            _employeeManager = employeeManager;
        }

        public async Task<BaseResult<WorkDay>> Create(WorkDay model)
        {
            var createResult = await _workDayProvider.Create(model);
            return new BaseResult<WorkDay>(createResult.Succed, model, createResult.OperationStatus);
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

        private HoursDaysInWorkMonth GetHoursDaysInWorkMonth(List<WorkDay> WorkDays) => new HoursDaysInWorkMonth(WorkDays.Sum(x => x.Hours), WorkDays.Where(x => x.Hours != 0f).ToList().Count());

        private IList<WorkDay> GetWorkDaysWithEmployees(IList<BetEmployee> employees)
        {
            var workDays = new List<WorkDay>();
            foreach (var item in employees)
            {
                workDays.AddRange(GetWorkDays(item));
            }
            return workDays;
        }

        public async Task<CreateWorkDaysResult> CreateNewWorkDays()
        {
            var getEmployeesResult = await _employeeManager.GetBetEmployees();
            var workDays = GetWorkDaysWithEmployees(getEmployeesResult.Data);
            var hoursDaysInWorkMonth = GetHoursDaysInWorkMonth(GetWorkDays(getEmployeesResult.Data.First()));
            var createResult = await _workDayProvider.CreateRange(workDays);
            return new CreateWorkDaysResult(createResult.Succed, workDays, OperationStatuses.Ok, getEmployeesResult.Data, hoursDaysInWorkMonth);
        }

        public async Task<BaseResult<bool>> Delete(Guid id) => await _workDayProvider.Delete(id);
        

        public async Task<BaseResult<IList<WorkDay>>> GetAll()
        {
            var getAllResult = await _workDayProvider.GetAll();
            return new BaseResult<IList<WorkDay>>(getAllResult.Succed, getAllResult.Data, getAllResult.OperationStatus);
        }

        public Task<BaseResult<WorkDay>> GetById(Guid id) => _workDayProvider.GetById(id);
        

        public async Task<BaseResult<WorkDay>> Update(WorkDay model)
        {
            var updateResult = await _workDayProvider.Update(model);
            return new BaseResult<WorkDay>(updateResult.Succed, model, updateResult.OperationStatus);
        }
        public async Task<BaseResult<IList<WorkDay>>> UpdateRange(IList<WorkDay> workDays)
        {
            var updateResult = await _workDayProvider.UpdateRange(workDays);
            return new BaseResult<IList<WorkDay>>(updateResult.Succed, workDays, OperationStatuses.Ok);
        }
    }
}
