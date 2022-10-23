using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Accounting.DAL.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
#nullable disable
        private readonly ApplicationDBContext _dbContext;
        public DocumentRepository(ApplicationDBContext dbContext) => _dbContext = dbContext;
        
        public async Task Add(Document entity)
        {
            _dbContext.AttachRange(entity.Employees);
            _dbContext.AttachRange(entity.Accruals);
            await _dbContext.Documents.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var documnetToDelete = await _dbContext.Documents.Include(x => x.Accruals).Include(x => x.Employees).SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.Documents.Remove(documnetToDelete);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<Document>> GetSearchDocumentsByPredicate(Expression<Func<Document, bool>> queryWhere) => await _dbContext.Documents.Where(queryWhere).ToListAsync();
        public async Task<IEnumerable<Document>> ReadAll() => await _dbContext.Documents.OrderByDescending(x => x.DateCreate).ToListAsync();

        public async Task<Document> ReadById(Guid id) => await _dbContext.Documents.Include(x => x.Employees).Include(x => x.Accruals).SingleOrDefaultAsync(x => x.Id == id);

        public async Task Update(Document entity)
        {
            _dbContext.AttachRange(entity.Accruals);
            _dbContext.AttachRange(entity.Employees);
            _dbContext.Documents.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
