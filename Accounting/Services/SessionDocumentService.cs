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
        private const string sessionDocumentKey = "sessionDocumentKey";
        public SessionDocumentService(IServiceProvider provider) => _session = provider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

        public async Task<bool> AddEmployee(NotBetEmployee employee)
        {
            try
            {
                var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey) ?? new SessionDocument();
                sessionDocument.Employees.Add(this.MapToSessionEmployee(employee));
                _session.SetJson(sessionDocumentKey, sessionDocument);
                await _session.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey);
            var document = new Document(this.MapToListFromSessionEmployee(sessionDocument.Employees),this.MapToListFromSessionAccrual(sessionDocument.Accruals)
                ,documentViewModel.Name, documentViewModel.DateCreate.Date);
            return document;
        }

        public async Task<bool> CreateSessionDocument()
        {
            try
            {
                _session.SetJson(sessionDocumentKey, new SessionDocument());
                await _session.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Accrual> GetAccrualsByEmployeeId(Guid employeeId)
        {
            var sessionAccruals = _session.GetJson<SessionDocument>(sessionDocumentKey).Accruals.Where(x => x.EmployeeId == employeeId);
            if (sessionAccruals is not null)
                return this.MapToListFromSessionAccrual(sessionAccruals.ToList());
            return null;
        }
        public async Task<bool> AddAccrual(Accrual accrual)
        {
            try
            {
                var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey);
                sessionDocument.Accruals.Add(this.MapToSessionAccrual(accrual));
                _session.SetJson(sessionDocumentKey, sessionDocument);
                await _session.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public decimal SumOfAccruals() => _session.GetJson<SessionDocument>(sessionDocumentKey).Accruals.Sum(x => x.Ammount);
        public async Task<bool> DeleteEmployee(Guid id)
        {
            try
            {
                var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey);
                sessionDocument.Employees.Remove(sessionDocument.Employees.FirstOrDefault(x => x.Id == id));
                _session.SetJson(sessionDocumentKey, sessionDocument);
                await _session.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateAccrual(decimal ammount, Guid accrualId)
        {
            try
            {
                var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey);
                sessionDocument.Accruals.SingleOrDefault(x => x.Id == accrualId).Ammount = ammount;
                _session.SetJson(sessionDocumentKey, sessionDocument);
                await _session.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAccrualsByEmployeeId(Guid id)
        {
            try
            {
                var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey);
                sessionDocument.Accruals.RemoveAll(x => x.EmployeeId == id);
                _session.SetJson(sessionDocumentKey, sessionDocument);
                await _session.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAccrual(Guid accrualId)
        {
            try
            {
                var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey);
                sessionDocument.Accruals.RemoveAll(x => x.Id == accrualId);
                _session.SetJson(sessionDocumentKey, sessionDocument);
                await _session.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private SessionAccrual MapToSessionAccrual(Accrual accrual)
        {
            var sessionAccrual = new SessionAccrual()
            {
                Ammount = accrual.Ammount,
                Id = accrual.Id,
                DateCreate = accrual.DateCreate,
                EmployeeId = accrual.EmployeeId,
            };
            return sessionAccrual;
        }
        
        private Accrual MapFromSessionAccrual(SessionAccrual sessionAccrual) => new Accrual(sessionAccrual.DateCreate, sessionAccrual.Ammount, sessionAccrual.Id);
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
            var employee = new NotBetEmployee(sessionEmployee.Name, sessionEmployee.InnerId);
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
