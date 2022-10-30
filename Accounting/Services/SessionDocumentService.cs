using Accounting.Domain.Models;
using Accounting.Domain.SessionEntity;
using Accounting.Domain.ViewModels;
using Accounting.Extensions;
using Accounting.Services.Interfaces;

namespace Accounting.Services
{
    public class SessionDocumentService : ISessionDocumentService
    {
#nullable disable
        private readonly ISession _session;
        private readonly ILogger<SessionDocument> _logger;
        private const string sessionDocumentKey = "sessionDocumentKey";
        public SessionDocumentService(IServiceProvider provider, ILogger<SessionDocument> logger)
        {
            _session = provider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            _logger = logger;
        }
        
        public async Task<bool> AddEmployee(NotBetEmployee employee)
        {
            var sessionDocument = GetSessionDocument();
            sessionDocument.Employees.Add(this.MapToSessionEmployee(employee));
            return await Commit(sessionDocument);
        }
        public async Task<bool> Clear()
        {
            try
            {
                _session.Clear();
                await _session.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Document GetDocument(DocumentViewModel documentViewModel)
        {
            var sessionDocument = GetSessionDocument();
            var document = new Document(this.MapToListFromSessionEmployee(sessionDocument.Employees),this.MapToListFromSessionAccrual(sessionDocument.Accruals)
                ,documentViewModel.Name, documentViewModel.DateCreate.Date);
            return document;
        }
        public Document GetDocument(UpdateDocumentViewModel documentViewModel)
        {
            var sessionDocument = GetSessionDocument();
            return new Document(documentViewModel.Id, this.MapToListFromSessionEmployee(sessionDocument.Employees), 
                this.MapToListFromSessionAccrual(sessionDocument.Accruals), documentViewModel.Name, documentViewModel.DateCreate);
        }

        public async Task<bool> CreateSessionDocument() => await Commit(new SessionDocument());

        public List<Accrual> GetAccrualsByEmployeeId(Guid employeeId)
        {
            var sessionAccruals = GetSessionDocument().Accruals.Where(x => x.EmployeeId == employeeId); 
            if (sessionAccruals is not null)
                return this.MapToListFromSessionAccrual(sessionAccruals.ToList());
            return null;
        }

        public async Task<bool> AddAccrual(Accrual accrual)
        {
            var sessionDocument = GetSessionDocument();
            sessionDocument.Accruals.Add(this.MapToSessionAccrual(accrual));
            return await Commit(sessionDocument);
        }

        public decimal SumOfAccruals() => _session.GetJson<SessionDocument>(sessionDocumentKey).Accruals.Sum(x => x.Ammount);

        public async Task<bool> DeleteEmployee(Guid id)
        {
            var sessionDocument = GetSessionDocument();
            sessionDocument.Employees.Remove(sessionDocument.Employees.FirstOrDefault(x => x.Id == id));
            return await Commit(sessionDocument);
        }

        public async Task<bool> UpdateAccrual(decimal ammount, Guid accrualId)
        {
            var sessionDocument = GetSessionDocument();
            sessionDocument.Accruals.SingleOrDefault(x => x.Id == accrualId).Ammount = ammount;
            return await Commit(sessionDocument);
        }

        public async Task<bool> DeleteAccrualsByEmployeeId(Guid id)
        {
            var sessionDocument = GetSessionDocument();
            sessionDocument.Accruals.RemoveAll(x => x.EmployeeId == id);
            return await Commit(sessionDocument);
        }

        public async Task<bool> DeleteAccrual(Guid accrualId)
        {
            var sessionDocument = GetSessionDocument();
            sessionDocument.Accruals.RemoveAll(x => x.Id == accrualId);
            return await Commit(sessionDocument);
        }
        public async Task<bool> LoadDocument(Document document) => await Commit(new SessionDocument()
        {
            Accruals = MapToListFromAccruals(document.Accruals),
            Employees = MapToListFromEmployees(document.Employees)
        });

        private List<SessionAccrual> MapToListFromAccruals(List<Accrual> accruals)
        {
            var sessionAccruals = new List<SessionAccrual>();
            foreach (var item in accruals)
                sessionAccruals.Add(MapToSessionAccrual(item));
            return sessionAccruals;
        }

        private List<SessionNotBetEmployee> MapToListFromEmployees(List<NotBetEmployee> employees)
        {
            var sessionEmployees = new List<SessionNotBetEmployee>();
            foreach (var item in employees)
                sessionEmployees.Add(MapToSessionEmployee(item));
            return sessionEmployees;
        }

        private SessionDocument GetSessionDocument() => _session.GetJson<SessionDocument>(sessionDocumentKey);

        private async Task<bool> Commit(SessionDocument sessionDocument)
        {
            try
            {
                _session.SetJson(sessionDocumentKey, sessionDocument);
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

        private SessionAccrual MapToSessionAccrual(Accrual accrual)
        {
            var sessionAccrual = new SessionAccrual()
            {
                Ammount = accrual.Ammount,
                Id = accrual.Id,
                DateCreate = accrual.DateCreate,
                EmployeeId = accrual.EmployeeId,
                IsAdditional = accrual.IsAdditional
            };
            return sessionAccrual;
        }
        
        private Accrual MapFromSessionAccrual(SessionAccrual sessionAccrual) => new Accrual(sessionAccrual.DateCreate, sessionAccrual.Ammount, sessionAccrual.Id, sessionAccrual.IsAdditional);

        private SessionNotBetEmployee MapToSessionEmployee(NotBetEmployee employee)
        {
            var sessionEmployee = new SessionNotBetEmployee()
            {
                Name = employee.Name,
                Id = employee.Id,
                InnerId = employee.InnerId
            };
            return sessionEmployee;
        }

        private NotBetEmployee MapFromSessionEmployee(SessionNotBetEmployee sessionEmployee)
        {
            var employee = new NotBetEmployee(sessionEmployee.Name, sessionEmployee.InnerId, 0);
            employee.SetId(sessionEmployee.Id);
            return employee;
        }

        private List<NotBetEmployee> MapToListFromSessionEmployee(List<SessionNotBetEmployee> sessionEmployees)
        {
            List<NotBetEmployee> employees = new List<NotBetEmployee>();
            foreach (var item in sessionEmployees)
                employees.Add(this.MapFromSessionEmployee(item));
            return employees;
        }

        private List<Accrual> MapToListFromSessionAccrual(List<SessionAccrual> sessionAccruals)
        {
            List<Accrual> accruals = new List<Accrual>();
            foreach (var item in sessionAccruals)
                accruals.Add(this.MapFromSessionAccrual(item));
            return accruals;
        }
    }
}
