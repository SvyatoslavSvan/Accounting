using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Providers
{
    public class DocumentProvider : IBaseProvider<DocumentProvider>
    {
        private readonly IBaseRepository<Document> _documentRepository;
        public DocumentProvider(IBaseRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public Task<BaseResult<bool>> Create(DocumentProvider entity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<List<DocumentProvider>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<DocumentProvider>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<bool>> Update(DocumentProvider entity)
        {
            throw new NotImplementedException();
        }
    }
}
