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

        public async Task<BaseResult<Employee>> Create(Employee model)
        {
            var createResult = await _provider.Create(model);
            return new BaseResult<Employee>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id) => await _provider.Delete(id);

        public async Task<BaseResult<IList<Employee>>> GetAll()
        {
            var getAllResult = await _provider.GetAll();
            return new BaseResult<IList<Employee>>(getAllResult.Succed, getAllResult.Data, getAllResult.OperationStatus);
        }

        public async Task<BaseResult<BetEmployee>> GetBetEmployee(Guid id) => await _provider.GetBetEmployee(id);

        public async Task<BaseResult<IList<BetEmployee>>> GetBetEmployees() => await _provider.GetBetEmployees();

        public async Task<BaseResult<Employee>> GetById(Guid id) => await _provider.GetById(id);

        public async Task<BaseResult<IList<Employee>>> GetEmployeeWithSalaryPropertiesByPeriod(DateTime from, DateTime to) => await _provider.GetAllByPredicate(
                includeBetEmployee: x => x.Include(x => x.Timesheets.
                Where(x => x.Date.Year >= from.Date.Year && x.Date.Year <= to.Date.Year && x.Date.Month >= from.Date.Month && x.Date.Month <= to.Date.Month))
                .ThenInclude(x => x.WorkDays)
                .Include(x => x.Group).
                Include(x => x.Documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)).
                ThenInclude(x => x.Payouts).
                Include(x => x.Documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)).
                ThenInclude(x => x.Payouts),
            includeNotBetEmployee:
            x => x.Include(x => x.Group).
            Include(x => x.Documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)).
            ThenInclude(x => x.Payouts).
            Include(x => x.Documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date)).
            ThenInclude(x => x.Payouts)
            
            );

        public async Task<BaseResult<IList<BetEmployee>>> GetEmployeesWithWorkDaysByDate(DateTime date) => await _provider.GetBetEmployeesWithInclude(
            x => x.Include(x => x.Group).Include(x => x.WorkDays.Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year)
            ));

        public async Task<BaseResult<Employee>> GetNotBetEmployee(Guid id) => await _provider.GetNotBetEmployee(id);

        public async Task<BaseResult<IEnumerable<Employee>>> GetNotBetEmployees() => await _provider.GetNotBetEmployees();

        public async Task<BaseResult<IEnumerable<Employee>>> GetNotBetEmployeesIncludeDocument() => await _provider.GetNotBetEmployeesIncludeDocument();

        public async Task<BaseResult<Employee>> Update(Employee model)
        {
            var updateResult = await _provider.Update(model);
            return new BaseResult<Employee>(updateResult.Succed, model, updateResult.OperationStatus);
        }

        public Task<BaseResult<IList<Employee>>> GetSearch(EmployeeSearchRequest request) => _provider.GetAllByPredicate(
            betEmployeePredicate: GetSearchEmployeePredicate<BetEmployee>(request),
            notBetEmployeePredicate: GetSearchEmployeePredicate<Employee>(request), x => x.Include(x => x.Group), x => x.Include(x => x.Group), x => x.OrderBy(x => x.Name), x => x.OrderBy(x => x.Name));

        private Expression<Func<T, bool>> GetSearchEmployeePredicate<T>(EmployeeSearchRequest request) where T : Employee
        {
            var predicate = PredicateBuilder.True<T>();
            if (request.Name is not default(string))
            {
                predicate = predicate.And(x => x.Name.Contains(request.Name));
            }
            if (request.InnerId is not default(string))
            {
                predicate = predicate.And(x => x.InnerId == request.InnerId);
            }
            return predicate;
        }
    }
}
