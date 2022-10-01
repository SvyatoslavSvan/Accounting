using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Providers
{
#nullable disable
    public class DocumentProvider : IBaseProvider<Document>
    {
        private readonly IBaseRepository<Document> _documentRepository;
        public DocumentProvider(IBaseRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<BaseResult<bool>> Create(Document entity)
        {
            try
            {
                await _documentRepository.Add(entity);
                return new BaseResult<bool>(true, true);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            try
            {
                await _documentRepository.Delete(id);
                return new BaseResult<bool>(true, true);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }

        public async Task<BaseResult<List<Document>>> GetAll()
        {
            try
            {
                var documents = await _documentRepository.ReadAll();
                return new BaseResult<List<Document>>(false, documents.ToList());
            }
            catch (Exception)
            {
                return new BaseResult<List<Document>>(false, null);
            }
        }

        public async Task<BaseResult<Document>> GetById(Guid id)
        {
            try
            {
                var document = await _documentRepository.ReadById(id);
                if (document is null)
                    return new BaseResult<Document>(false, null);
                return new BaseResult<Document>(true, document);
            }
            catch (Exception)
            {
                return new BaseResult<Document>(false, null);
            }
        }

        public async Task<BaseResult<bool>> Update(Document entity)
        {
            try
            {
                await _documentRepository.Update(entity);
                return new BaseResult<bool>(true, true);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }
    }
}
