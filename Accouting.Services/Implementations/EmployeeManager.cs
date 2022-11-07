using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accouting.Domain.Managers.Implementations
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeProvider _provider;

        public EmployeeManager(IEmployeeProvider provider)
        {
            _provider = provider;
        }

        public async Task<BaseResult<EmployeeBase>> Create(EmployeeBase model)
        {
            var createResult = await _provider.Create(model);
            return new BaseResult<EmployeeBase>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id) => await _provider.Delete(id);

        public async Task<BaseResult<IList<EmployeeBase>>> GetAll()
        {
            var getAllResult = await _provider.GetAll();
            return new BaseResult<IList<EmployeeBase>>(getAllResult.Succed, getAllResult.Data, getAllResult.OperationStatus);
        }

        public async Task<BaseResult<BetEmployee>> GetBetEmployee(Guid id) => await _provider.GetBetEmployee(id);

        public async Task<BaseResult<IList<BetEmployee>>> GetBetEmployees() => await _provider.GetBetEmployees();

        public async Task<BaseResult<EmployeeBase>> GetById(Guid id) => await _provider.GetById(id);

        public async Task<BaseResult<IList<EmployeeBase>>> GetEmployeeWithSalaryPropertiesByPeriod(DateTime from, DateTime to) => await _provider.GetAllByPredicate(
                includeBetEmployee: x => x.Include(x => x.WorkDays.Where(x => x.Date.Date >= from.Date && x.Date.Date <= to.Date))
            .Include(x => x.DeducationDocuments.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)), 
            includeNotBetEmployee: x => x.Include(x => x.Documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)));

        public async Task<BaseResult<IList<BetEmployee>>> GetEmployeesWithWorkDaysByDate(DateTime date) => await _provider.GetBetEmployeesWithInclude(
            x => x.Include(x => x.Group).Include(x => x.WorkDays.Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year)
            ));

        public async Task<BaseResult<NotBetEmployee>> GetNotBetEmployee(Guid id) => await _provider.GetNotBetEmployee(id);

        public async Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployees() => await _provider.GetNotBetEmployees();

        public async Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployeesIncludeDocument() => await _provider.GetNotBetEmployeesIncludeDocument();

        public async Task<BaseResult<EmployeeBase>> Update(EmployeeBase model)
        {
            var updateResult = await _provider.Update(model);
            return new BaseResult<EmployeeBase>(updateResult.Succed, model, updateResult.OperationStatus);
        }
    }
}
