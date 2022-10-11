using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Providers
{
    public class AccrualProvider : IAccrualProvider
    {
#nullable disable
        private readonly IAccrualRepository _accrualRepository;
        public AccrualProvider(IAccrualRepository accrualRepository)
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

        public async Task<BaseResult<bool>> DeleteRange(List<Accrual> accruals)
        {
            try
            {
                await _accrualRepository.DeleteRange(accruals);
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
