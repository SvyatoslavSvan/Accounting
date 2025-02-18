﻿using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Extensions;
using Accounting.SessionEntity;

namespace Accounting.Services
{
    public class SessionDocumentService : ISessionDocumentService
    {
        private readonly ISession _session;
        private readonly ILogger<Document> _logger;
        private const string sessionDocumentKey = "sessiondocumentkey";

        public SessionDocumentService(IServiceProvider provider, ILogger<Document> logger)
        {
            _session = provider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            _logger = logger;
        }

        private IList<Employee> _employees
        {
            get
            {
                return _document.Employees;
            }
        }


        public Task<bool> Create() => Commit(new SessionDocument());


        private async Task<bool> Commit(SessionDocument document)
        {
            try
            {
                _session.SetJson(sessionDocumentKey, document);
                await _session.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex);
                return false;
            }
        }

        

        private SessionDocument _document => _session.GetJson<SessionDocument>(sessionDocumentKey);

        private void LogErrorMessage(Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex?.StackTrace);
            _logger.LogError(ex?.InnerException?.Message);
        }

        public async Task<bool> AddEmployeeToDocument(Employee employee)
        {
            var document = _document;
            document.AddEmployee(employee);
            return await Commit(document);
        }

        public List<Payout> GetPayoutsByEmployeeId(Guid employeeId) => _document.GetPayoutsByEmployeeId(employeeId);

        public async Task<bool> AddPayout(Payout payout)
        {
            var document = _document;
            document.AddPayout(payout);
            return await Commit(document);
        }

        public async Task<bool> UpdatePayout(Guid payoutId, decimal ammount)
        {
            var document = _document;
            document.UpdatePayout(payoutId, ammount);
            return await Commit(document);
        }

        public async Task<bool> DeleteAccrual(Guid payoutId)
        {
            var document = _document;
            document.DeletePayout(payoutId);
            return await Commit(document);
        }

        public async Task<bool> DeleteEmployee(Guid employeeId, Guid PayoutId)
        {
            var document = _document;
            document.DeleteEmployee(employeeId, PayoutId);
            return await Commit(document);
        }

        public SessionDocument GetDocument() => RemoveTwinsEmployees();

        private SessionDocument RemoveTwinsEmployees()
        {
            var document = _document;
            document.Employees = _document.Employees.DistinctBy(x => x.Id).ToList();
            return document;
        }

        public async Task<bool> LoadDocument(Document document) => await Commit(MapToSessionDocument(document));

        private SessionDocument MapToSessionDocument(Document document)
        {
            var sessionDocument = new SessionDocument();
            var employees = document.Employees;
            var payouts = document.Payouts;
            foreach (var item in employees)
            {
                var itemPayouts = payouts.Where(x => x.EmployeeId == item.Id).ToList();
                var payoutsCount = itemPayouts.Count();
                if (payoutsCount == 1)
                {
                    sessionDocument.AddEmployee(item);
                    sessionDocument.AddPayout(itemPayouts.First());
                }
                if(payoutsCount > 1)
                {
                    for (int i = 0; i < payoutsCount; i++)
                    {
                        sessionDocument.AddEmployee(item);
                    }
                    sessionDocument.AddPayouts(itemPayouts);
                }
            }
            return sessionDocument;
        }

        public decimal GetSumOfPayouts()
        {
            var document = _document;
            var sum = document.Payouts.Sum(x => x.Ammount);
            return sum;
        }

        public int GetCountOfTwinsEmployees(Guid id) => _employees.Where(x => x.Id == id).Count();

        public IList<Payout> GetPayouts() => _document.Payouts;

        public IList<Employee> GetEmployees() => _document.Employees;
    }
}
