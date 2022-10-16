using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.PredicatesBuilder;
using System;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Accounting.DAL.Providers
{
#nullable disable
    public class DocumentProvider : IDocumentProvider
    {
        private readonly IDocumentRepository _documentRepository;
        public DocumentProvider(IDocumentRepository documentRepository)
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
                return new BaseResult<List<Document>>(true, documents.ToList());
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

        public async Task<BaseResult<IEnumerable<Document>>> GetBySearchDocuments(DateTime from, DateTime to, string name, DateTime dateCreate)
        {
            try
            {
                var predicate = BuildSearchPredicate(from, to, name, dateCreate);
                var searchedDocuments = await _documentRepository.GetSearchDocumentsByPredicate(predicate);
                return new BaseResult<IEnumerable<Document>>(true, searchedDocuments);
            }
            catch (Exception)
            {
                return new BaseResult<IEnumerable<Document>>(false, null);
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
        private Expression<Func<Document, bool>> BuildSearchPredicate(DateTime from, DateTime to, string name, DateTime dateCreate)
        {
            var predicate = PredicateBuilder.True<Document>();
            if (from != default(DateTime))
                predicate = predicate.And(x => x.DateCreate >= from);
            if (to != default(DateTime))
                predicate = predicate.And(x => x.DateCreate <= to);
            if (!string.IsNullOrWhiteSpace(name))
                predicate = predicate.And(x => x.Name.Contains(name));
            if (dateCreate != default(DateTime) && from == default(DateTime) && to == default(DateTime))
                predicate = predicate.And(x => x.DateCreate == dateCreate);
            return predicate;
        }
    }
}
