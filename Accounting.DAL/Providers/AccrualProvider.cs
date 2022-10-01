using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Providers
{
    public class AccrualProvider : IBaseProvider<Accrual>
    {
#nullable disable
        private readonly IBaseRepository<Accrual> _accrualRepository;
        public AccrualProvider(IBaseRepository<Accrual> accrualRepository)
        {
            _accrualRepository = accrualRepository;
        }

        public async Task<BaseResult<bool>> Create(Accrual entity)
        {
            try
            {
                await _accrualRepository.Add(entity);
                return new BaseResult<bool>(true, true);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            try
            {
                await _accrualRepository.Delete(id);
                return new BaseResult<bool>(true, true);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }

        public async Task<BaseResult<List<Accrual>>> GetAll()
        {
            try
            {
                var accruals = await _accrualRepository.ReadAll();
                return new BaseResult<List<Accrual>>(true, accruals.ToList());
            }
            catch (Exception)
            {
                return new BaseResult<List<Accrual>>(false, null);
            }
        }

        public async Task<BaseResult<Accrual>> GetById(Guid id)
        {
            try
            {
                var accrual = await _accrualRepository.ReadById(id);
                return new BaseResult<Accrual>(true, accrual);
            }
            catch (Exception)
            {
                return new BaseResult<Accrual>(true, null);
            }
        }

        public async Task<BaseResult<bool>> Update(Accrual entity)
        {
            try
            {
                await _accrualRepository.Update(entity);
                return new BaseResult<bool>(true, true);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }
    }
}
