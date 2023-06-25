using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Accounting.DAL.Providers
{
    public class EmployeeProvider : ProviderBase, IEmployeeProvider
    {
#nullable disable

        public EmployeeProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<Employee> logger) : base(unitOfWork, logger) { }

        public async Task<BaseResult<bool>> Create(Employee entity)
        {
            _unitOfWork.DbContext.Attach(entity.Group);
            await _unitOfWork.GetRepository<Employee>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<Employee>>> GetAll()
        {
            try
            {
                var employees = await _unitOfWork.GetRepository<Employee>().GetAllAsync(disableTracking: false, orderBy: x => x.OrderBy(x => x.Name), include: x => x.Include(x => x.Group));
                return new BaseResult<List<Employee>>(true, employees.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<Employee>>(ex);
            }
        }

        public async Task<BaseResult<Employee>> GetById(Guid id)
        {
            try
            {
                var employee = await _unitOfWork.GetRepository<Employee>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
                return new BaseResult<Employee>(true, employee, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<Employee>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(Employee entity)
        {
            _unitOfWork.DbContext.Attach(entity.Group);
            await _unitOfWork.GetRepository<Employee>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<Employee>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<Employee>> GetNotBetEmployee(Guid id)
        {
            try
            {
                var employee = await _unitOfWork.DbContext.Employees.OfType<Employee>().FirstOrDefaultAsync(x => x.Id == id);
                if (employee is null)
                    return new BaseResult<Employee>(false, null, OperationStatuses.NotFound);
                return new BaseResult<Employee>(true, employee, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<Employee>(ex);
            }
        }

        public async Task<BaseResult<IEnumerable<Employee>>> GetNotBetEmployees()
        {
            try
            {
                return new BaseResult<IEnumerable<Employee>>(true, await _unitOfWork.DbContext.Employees.OfType<Employee>().ToListAsync(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IEnumerable<Employee>>(ex);
            }
        }

        public async Task<BaseResult<IEnumerable<Employee>>> GetNotBetEmployeesIncludeDocument()
        {
            try
            {
                return new BaseResult<IEnumerable<Employee>>(true, await _unitOfWork.DbContext.Employees.OfType<Employee>().Include(x => x.Documents).ToListAsync());
            }
            catch (Exception ex)
            {
                return HandleException<IEnumerable<Employee>>(ex);
            }
        }

        public async Task<BaseResult<BetEmployee>> GetBetEmployee(Guid id)
        {
            try
            {
                var employee = await _unitOfWork.GetRepository<BetEmployee>().GetFirstOrDefaultAsync(predicate: x => x.Id == id, disableTracking: false);
                if (employee is null)
                    return new BaseResult<BetEmployee>(true, employee, OperationStatuses.NotFound);
                return new BaseResult<BetEmployee>(true, employee, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<BetEmployee>(ex);
            }
        }

        public async Task<BaseResult<IList<BetEmployee>>> GetBetEmployees()
        {
            try
            {
                return new BaseResult<IList<BetEmployee>>(true, await _unitOfWork.GetRepository<BetEmployee>().GetAllAsync(true), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<BetEmployee>>(ex);
            }
        }

        public async Task<BaseResult<IList<BetEmployee>>> GetEmployeesWithWorkDaysByDate(DateTime date)
        {
            try
            {
                return new BaseResult<IList<BetEmployee>>(true, await _unitOfWork.GetRepository<BetEmployee>().GetAllAsync(include: x => x
                .Include(x => x.WorkDays.Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year))), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<BetEmployee>>(ex);
            }
        }

        public async Task<BaseResult<IList<BetEmployee>>> GetBetEmployeesWithInclude(Func<IQueryable<BetEmployee>, IIncludableQueryable<BetEmployee, object>> include)
        {
            try
            {
                return new BaseResult<IList<BetEmployee>>(true, await _unitOfWork.GetRepository<BetEmployee>().GetAllAsync(include: include), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<BetEmployee>>(ex);
            }
        }

        public async Task<BaseResult<IList<Employee>>> GetAllByPredicate(Expression<Func<BetEmployee, bool>> betEmployeePredicate = null,
            Expression<Func<Employee, bool>> notBetEmployeePredicate = null, Func<IQueryable<BetEmployee>,
                IIncludableQueryable<BetEmployee, object>> includeBetEmployee = null, Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>> includeNotBetEmployee = null,
            Func<IQueryable<BetEmployee>, IOrderedQueryable<BetEmployee>> orderByBetEmployee = null,
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderByNotBetEmployee = null)
        {
            try
            {
                var employees = new List<Employee>();
                var betEmployees = await _unitOfWork.GetRepository<BetEmployee>().GetAllAsync(predicate: betEmployeePredicate, include: includeBetEmployee, orderBy: orderByBetEmployee);
                var notBetEmployees = await _unitOfWork.GetRepository<Employee>().GetAllAsync(predicate: notBetEmployeePredicate, include: includeNotBetEmployee, orderBy: orderByNotBetEmployee);
                foreach (var item in betEmployees)
                {
                    notBetEmployees.Remove(notBetEmployees.FirstOrDefault(x => x.Id == item.Id));
                }
                employees.AddRange(betEmployees);
                employees.AddRange(notBetEmployees);
                return new BaseResult<IList<Employee>>(true, employees, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<Employee>>(ex);
            }
        }

       
    }
    }
