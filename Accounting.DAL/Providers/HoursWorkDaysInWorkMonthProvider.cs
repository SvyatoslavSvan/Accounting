using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Accounting.DAL.Providers
{
    public class HoursWorkDaysInWorkMonthProvider : ProviderBase ,IBaseProvider<HoursDaysInWorkMonth>
    {
        public HoursWorkDaysInWorkMonthProvider(IUnitOfWork<ApplicationDBContext> unitOfwork, ILogger<HoursDaysInWorkMonth> logger) : base(unitOfwork, logger) { } 

        public Task<BaseResult<bool>> Create(HoursDaysInWorkMonth entity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<List<HoursDaysInWorkMonth>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<HoursDaysInWorkMonth>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<bool>> Update(HoursDaysInWorkMonth entity)
        {
            throw new NotImplementedException();
        }
    }
}
