using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.Requests;
using Accouting.Domain.Managers.Interfaces;
using Calabonga.PredicatesBuilder;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                includeBetEmployee: x => x.Include(x => x.Timesheets).ThenInclude(x => x.WorkDays)
                .Include(x => x.Group).
                Include(x => x.Documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)).
                ThenInclude(x => x.PayoutsBetEmployees).
                Include(x => x.Documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)).
                ThenInclude(x => x.PayoutsNotBetEmployees),
            includeNotBetEmployee:
            x => x.Include(x => x.Group).
            Include(x => x.Documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)).
            ThenInclude(x => x.PayoutsBetEmployees).
            Include(x => x.Documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)).
            ThenInclude(x => x.PayoutsNotBetEmployees)
            );

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

        public Task<BaseResult<IList<EmployeeBase>>> GetSearch(EmployeeSearchRequest request) => _provider.GetAllByPredicate(
            betEmployeePredicate: GetSearchEmployeePredicate<BetEmployee>(request),
            notBetEmployeePredicate: GetSearchEmployeePredicate<NotBetEmployee>(request), x => x.Include(x => x.Group), x => x.Include(x => x.Group), x => x.OrderBy(x => x.Name), x => x.OrderBy(x => x.Name));

        private Expression<Func<T, bool>> GetSearchEmployeePredicate<T>(EmployeeSearchRequest request) where T : EmployeeBase
        {
            var predicate = PredicateBuilder.True<T>();
            if (request.Name is not default(string))
            {
                predicate = predicate.And(x => x.Name.Contains(request.Name));
            }
            if (request.InnerId is not default(string))
            {
                predicate = predicate.And(x => x.InnerId.Contains(request.InnerId));
            }
            return predicate;
        }
    }
}
