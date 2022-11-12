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

        public async Task<bool> AddEmployeeToDocument(EmployeeBase employee)
        {
            var document = _document;
            document.AddEmployee(employee);
            return await Commit(document);
        }

        public List<PayoutBase> GetAccrualsByEmployeeId(Guid employeeId) => _document.GetPayoutsByEmployeeId(employeeId);

        public async Task<bool> AddPayout(PayoutBase payout)
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
            document.DeleteAccrual(payoutId);
            return await Commit(document);
        }
    }
}
