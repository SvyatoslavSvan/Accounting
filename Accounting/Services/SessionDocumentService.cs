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
            var document = new Document(documentViewModel.Name, documentViewModel.DateCreate);
            document.AddEmployeesToDocument(this.MapToListFromSessionEmployee(sessionDocument.Employees));
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

    }
}
