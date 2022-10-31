using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models.Base;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Accounting.DAL.Providers
{
    public class DeducationProvider : ProviderBase , IDeducationProvider
    {
#nullable disable
        public DeducationProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<DeducationBase> logger) : base(unitOfWork, logger) { }

        public Task<BaseResult<bool>> Create(DeducationBase entity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<List<DeducationBase>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<DeducationBase>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<bool>> Update(DeducationBase entity)
        {
            throw new NotImplementedException();
        }
    }
}
