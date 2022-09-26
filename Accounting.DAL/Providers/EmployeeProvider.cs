using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using System.Runtime.CompilerServices;

namespace Accounting.DAL.Providers
{
    public class EmployeeProvider : IEmployeeProvider
    {
#nullable disable
        private readonly IBaseEmployeeRepository<BetEmployee> _betEmployeeRepository;
        private readonly IBaseEmployeeRepository<NotBetEmployee> _notBetEmployeeRepository;
        public EmployeeProvider(IBaseEmployeeRepository<BetEmployee> betEmployeeRepository,
            IBaseEmployeeRepository<NotBetEmployee> notBetEmployeeRepository)
        {
            _betEmployeeRepository = betEmployeeRepository;
            _notBetEmployeeRepository = notBetEmployeeRepository;
        }

        public async Task<BaseResult<bool>> Create(EmployeeBase entity)
        {
            try
            {
                var isUniq = await IsUniqInnerId(entity);
                if (isUniq)
                {
                    if (entity is BetEmployee betEmployee)
                    {
                        await _betEmployeeRepository.Add(betEmployee);
                        return new BaseResult<bool>(true, true);
                    }
                    else if (entity is NotBetEmployee notBetEmployee)
                    {
                        await _notBetEmployeeRepository.Add(notBetEmployee);
                        return new BaseResult<bool>(true, true);
                    }
                }
                return new BaseResult<bool>(false, false);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }

        private async Task<bool> IsUniqInnerId(EmployeeBase entity)
        {
            if(entity is BetEmployee betEmployee)
                return await _betEmployeeRepository.CountGroupsWithInnerId(entity.InnerId) == 0 ? true : false;
            if(entity is NotBetEmployee notBetEmployee)
                return await _notBetEmployeeRepository.CountGroupsWithInnerId(entity.InnerId) == 0 ? true : false;
            return false;
        }

        public async Task<BaseResult<List<EmployeeBase>>> GetAll()
        {
            try
            {
                var betEmployees = await _betEmployeeRepository.ReadAll();
                var notBetEmployees = await _notBetEmployeeRepository.ReadAll();
                List<EmployeeBase> employees = new List<EmployeeBase>();
                employees.AddRange(betEmployees);
                employees.AddRange(notBetEmployees);
                employees.OrderBy(x => x.Name);
                return new BaseResult<List<EmployeeBase>>(true, employees);
            }
            catch (Exception)
            {
                return new BaseResult<List<EmployeeBase>>(false, null);
            }
        }

        public async Task<BaseResult<EmployeeBase>> GetById(Guid id)
        {
            try
            {
                var betEmployee = await _betEmployeeRepository.ReadById(id);
                if (betEmployee is not null)
                    return new BaseResult<EmployeeBase>(true, betEmployee);
                var notBetEmployee = await _notBetEmployeeRepository.ReadById(id);
                if (notBetEmployee is not null)
                    return new BaseResult<EmployeeBase>(true, notBetEmployee);
                return new BaseResult<EmployeeBase>(false, null);
            }
            catch (Exception)
            {
                return new BaseResult<EmployeeBase>(false, null);
            }
            
        }

        public async Task<BaseResult<bool>> Update(EmployeeBase entity)
        {
            try
            {
                if (entity is BetEmployee betEmployee)
                {
                    await _betEmployeeRepository.Update(betEmployee);
                    return new BaseResult<bool>(true, true);
                }
                else if (entity is NotBetEmployee notBetEmployee)
                {
                    await _notBetEmployeeRepository.Update(notBetEmployee);
                    return new BaseResult<bool>(true, true);
                }
                return new BaseResult<bool>(false, false);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }

        public async Task<BaseResult<NotBetEmployee>> GetNotBetEmployeeById(Guid Id)
        {
            try
            {
                var notBetEmployee = await _notBetEmployeeRepository.ReadById(Id);
                if (notBetEmployee is not null)
                    return new BaseResult<NotBetEmployee>(true, notBetEmployee);
                return new BaseResult<NotBetEmployee>(false, null);
            }
            catch (Exception)
            {
                return new BaseResult<NotBetEmployee>(false, null);
            }
        }

        public async Task<BaseResult<BetEmployee>> GetBetEmployeeById(Guid id)
        {
            try
            {
                var betEmployee = await _betEmployeeRepository.ReadById(id);
                if (betEmployee is not null)
                    return new BaseResult<BetEmployee>(true, betEmployee);
                return new BaseResult<BetEmployee>(false, null);
            }
            catch (Exception)
            {
                return new BaseResult<BetEmployee>(false, null);
            }
        }
        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            try
            {
                var betEmployeeToDelete = await _betEmployeeRepository.ReadById(id);
                if (betEmployeeToDelete is not null)
                {
                    await _betEmployeeRepository.Delete(id);
                    return new BaseResult<bool>(true, true);
                }
                var notBetEmployeeToDelete = await _notBetEmployeeRepository.ReadById(id);
                if (notBetEmployeeToDelete is not null)
                {
                    await _notBetEmployeeRepository.Delete(id);
                    return new BaseResult<bool>(true, true);
                }
                return new BaseResult<bool>(false, false);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }

        }

        public async Task<BaseResult<NotBetEmployee>> getNotBetEmployee(Guid id)
        {
            try
            {
                var employee = await _notBetEmployeeRepository.ReadById(id);
                return new BaseResult<NotBetEmployee>(true, employee);
            }
            catch (Exception)
            {
                return new BaseResult<NotBetEmployee>(false, null);
            }
        }

        public async Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployees()
        {
            try
            {
                var employees = await _notBetEmployeeRepository.ReadAll();
                return new BaseResult<IEnumerable<NotBetEmployee>>(true, employees);
            }
            catch (Exception)
            {
                return new BaseResult<IEnumerable<NotBetEmployee>>(false, null);
            }
        }
    }
}
