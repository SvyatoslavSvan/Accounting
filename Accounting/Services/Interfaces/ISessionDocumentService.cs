using Accounting.Domain.Models;
using Accounting.Domain.SessionEntity;
using Accounting.Domain.ViewModels;

namespace Accounting.Services.Interfaces
{
    public interface ISessionDocumentService
    {
        public Task<bool> AddEmployee(NotBetEmployee employee);
        public Document GetDocument(DocumentViewModel documentViewModel);
        public Task<bool> Clear();
        public Task<bool> CreateSessionDocument();

    }
}
