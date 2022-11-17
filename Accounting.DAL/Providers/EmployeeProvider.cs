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
    public class EmployeeProvider : ProviderBase,IEmployeeProvider
    {
#nullable disable
        
        
        public EmployeeProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<EmployeeBase> logger) : base(unitOfWork, logger) { }
       

        public async Task<BaseResult<bool>> Create(EmployeeBase entity)
        {
            var isUniq = await IsUniqInnerId(entity);
            if (isUniq)
            {
                _unitOfWork.DbContext.Attach(entity.Group);
                if (entity is BetEmployee betEmployee)
                {
                    await _unitOfWork.GetRepository<BetEmployee>().InsertAsync(betEmployee);
                }
                else if (entity is NotBetEmployee notBetEmployee)
                {
                    await _unitOfWork.GetRepository<NotBetEmployee>().InsertAsync(notBetEmployee);  
                }
                await _unitOfWork.SaveChangesAsync();
                if (!_unitOfWork.LastSaveChangesResult.IsOk)
                {
                    return HandleException<bool>();
                }
                return new BaseResult<bool>(true, true, OperationStatuses.Ok);
            }
            return new BaseResult<bool>(false, false, OperationStatuses.NotUniqInnerId);
        }

        

        private async Task<bool> IsUniqInnerId(EmployeeBase entity) => await CheckInnerId(entity);

        private async Task<bool> CheckInnerId(EmployeeBase entity)
        {
            if (entity is BetEmployee)
                return await CountOfEmployee<BetEmployee>(x => x.InnerId == entity.InnerId) == 0 ? true : false;
            if (entity is NotBetEmployee)
                return await CountOfEmployee<NotBetEmployee>(x => x.InnerId == entity.InnerId) == 0 ? true : false;
            return false;
        }

        private async Task<int> CountOfEmployee<TRepository>(Expression<Func<TRepository, bool>> predicate) where TRepository : EmployeeBase => await _unitOfWork.GetRepository<TRepository>()
            .CountAsync(predicate: predicate);

        public async Task<BaseResult<List<EmployeeBase>>> GetAll()
        {
            try
            { 
                var employees = new List<EmployeeBase>();
                employees.AddRange(await _unitOfWork.GetRepository<NotBetEmployee>().GetAllAsync(include: x => x.Include(x => x.Group), orderBy: x => x.OrderBy(x => x.Name)));
                employees.AddRange(await _unitOfWork.GetRepository<BetEmployee>().GetAllAsync(include: x => x.Include(x => x.Group), orderBy: x => x.OrderBy(x => x.Name)));
                employees.OrderBy(x => x.Name);
                return new BaseResult<List<EmployeeBase>>(true, employees, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<EmployeeBase>>(ex);
            }
        }

        public async Task<BaseResult<EmployeeBase>> GetById(Guid id)
        {
            try
            {
                var count = await _unitOfWork.GetRepository<BetEmployee>().CountAsync(x => x.Id == id);
                if (count > 0)
                {
                    return await GetEmployee<BetEmployee>(id);
                }
                else
                {
                    return await GetEmployee<NotBetEmployee>(id);
                }
            }
            catch (Exception ex)
            {
                return HandleException<EmployeeBase>(ex);
            }
        }

        private async Task<BaseResult<EmployeeBase>> GetEmployee<TRepository>(Guid id) where TRepository : EmployeeBase
        {
            var employee = await _unitOfWork.GetRepository<TRepository>().GetFirstOrDefaultAsync(predicate: x => x.Id == id, disableTracking: false);
            if (employee is null)
                return new BaseResult<EmployeeBase>(false, null, OperationStatuses.NotFound);
            return new BaseResult<EmployeeBase>(true, employee, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Update(EmployeeBase entity)
        {
            _unitOfWork.DbContext.Attach(entity.Group);
            SelectEmployeeRepository(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        private void SelectEmployeeRepository(EmployeeBase entity)
        {
            if (entity is BetEmployee betEmployee)
                UpdateEmployee<BetEmployee>(betEmployee);
            if (entity is NotBetEmployee notBetEmployee)
                UpdateEmployee<NotBetEmployee>(notBetEmployee);
        }

        private void UpdateEmployee<TRepository>(TRepository employee) where TRepository : EmployeeBase => _unitOfWork.GetRepository<TRepository>().Update(employee);

        

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            await ChooseRepositoryToDelete(id);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        private async Task ChooseRepositoryToDelete(Guid id)
        {
            var countOfBetEmployees = await CountOfEmployee<BetEmployee>(x => x.Id == id);
            if (countOfBetEmployees > 0)
                DeleteEmployee<BetEmployee>(id);
            else
                DeleteEmployee<NotBetEmployee>(id);
        }

        private void DeleteEmployee<TRepository>(Guid id) where TRepository : EmployeeBase => _unitOfWork.GetRepository<TRepository>().Delete(id);

        public async Task<BaseResult<NotBetEmployee>> GetNotBetEmployee(Guid id)
        {
            try
            {
                var employee = await _unitOfWork.GetRepository<NotBetEmployee>().GetFirstOrDefaultAsync(predicate: x => x.Id == id, disableTracking: false);
                if (employee is null)
                    return new BaseResult<NotBetEmployee>(false, null, OperationStatuses.NotFound);
                return new BaseResult<NotBetEmployee>(true, employee, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<NotBetEmployee>(ex);
            }
        }

        public async Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployees()
        {
            try
            {
                return new BaseResult<IEnumerable<NotBetEmployee>>(true, await _unitOfWork.
                    GetRepository<NotBetEmployee>().GetAllAsync(include: x => x.Include(x => x.Group), disableTracking: false), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IEnumerable<NotBetEmployee>>(ex);
            }
        }

        public async Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployeesIncludeDocument()
        {
            try
            {
                return new BaseResult<IEnumerable<NotBetEmployee>>(true, await _unitOfWork.
                    GetRepository<NotBetEmployee>().GetAllAsync(include: x => x.Include(x => x.Documents), disableTracking: false), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IEnumerable<NotBetEmployee>>(ex);
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

        public async Task<BaseResult<IList<EmployeeBase>>> GetAllByPredicate(Expression<Func<BetEmployee, bool>> betEmployeePredicate = null, 
            Expression<Func<NotBetEmployee, bool>> notBetEmployeePredicate = null, Func<IQueryable<BetEmployee>, 
                IIncludableQueryable<BetEmployee, object>> includeBetEmployee = null, Func<IQueryable<NotBetEmployee>, IIncludableQueryable<NotBetEmployee, object>> includeNotBetEmployee = null)
        {
            try
            {
                var employees = new List<EmployeeBase>();
                employees.AddRange(await _unitOfWork.GetRepository<BetEmployee>().GetAllAsync(predicate: betEmployeePredicate, include: includeBetEmployee));
                employees.AddRange(await _unitOfWork.GetRepository<NotBetEmployee>().GetAllAsync(predicate: notBetEmployeePredicate, include: includeNotBetEmployee));
                return new BaseResult<IList<EmployeeBase>>(true, employees, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<EmployeeBase>>(ex);
            }
        }
    }
}
