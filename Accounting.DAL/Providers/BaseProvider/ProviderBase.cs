using Accounting.DAL.Contexts;
using Accounting.DAL.Result.Provider.Base;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Accounting.DAL.Providers.BaseProvider
{
    public abstract class ProviderBase
    {
        protected readonly IUnitOfWork<ApplicationDBContext> _unitOfWork;
        protected readonly ILogger _logger;
        public ProviderBase(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        private void LogErrorMessage(Exception ex = null)
        {
            var excpetion = ex ?? _unitOfWork.LastSaveChangesResult.Exception;
            _logger.LogError(excpetion?.Message);
            _logger.LogError(excpetion?.InnerException?.Message ?? string.Empty);
            _logger.LogError(excpetion?.StackTrace);
        }

        protected BaseResult<T> HandleException<T>(Exception ex = null)
        {
            LogErrorMessage(ex);
            return new BaseResult<T>(false, default(T), OperationStatuses.Error);
        }
    }
}
