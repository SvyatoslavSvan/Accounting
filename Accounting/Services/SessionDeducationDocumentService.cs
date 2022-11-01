using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.SessionEntity;
using Accounting.Extensions;
using Accounting.Services.Interfaces;

namespace Accounting.Services
{
    public class SessionDeducationDocumentService : ISessionDeducationDocumentService
    {
        private const string _DeducationDocumentKey = "DeducationDocumentKey";
        private readonly ISession _session;
        private readonly ILogger<SessionDeducationDocument> _logger;
        public SessionDeducationDocumentService(IServiceProvider serviceProvider, ILogger<SessionDeducationDocument> logger)
        {
            _session = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            _logger = logger;
        }
        public async Task<bool> CreateSessionDeducationDocument()
        {
            return await Commit(new SessionDeducationDocument());
        }

        public async Task<bool> AddEmployee(EmployeeBase employee)
        {
            var document = GetDocument();
            employee.ToSerializable();
            if (employee is BetEmployee betEmployee)
            {
                document.BetEmployees.Add(betEmployee);
            }
            if(employee is NotBetEmployee notBetEmployee)
            {
                document.NotBetEmployees.Add(notBetEmployee);
            }
            return await Commit(document);
        }

        public async Task<bool> DeleteEmployee(EmployeeBase employee)
        {
            var document = GetDocument();
            document.RemoveEmployee(employee);
            return await Commit(document);
        }
        

        private async Task<bool> Commit(SessionDeducationDocument document)
        {
            try
            {
                _session.SetJson(_DeducationDocumentKey, document);
                await _session.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex);
                return false;
            }
        }

        private void LogErrorMessage(Exception exception)
        {
            _logger.LogError(exception.Message);
            _logger.LogError(exception.InnerException.Message ?? string.Empty);
            _logger.LogError(exception.StackTrace);
        }

        private SessionDeducationDocument GetDocument() => _session.GetJson<SessionDeducationDocument>(_DeducationDocumentKey);

        public EmployeeBase GetEmployeeById(Guid id)
        {
            var document = GetDocument();
            var employee = document.GetEmployee(id);
            return employee;
        }

        public List<DeducationBase> GetDeducationsByEmployeeId(Guid employeeId) => GetDocument().getDeducationsByEmployeeId(employeeId);
    }
}
