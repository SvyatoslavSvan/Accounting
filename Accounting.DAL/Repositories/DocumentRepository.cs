using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.DAL.Repositories
{
    public class DocumentRepository : IBaseRepository<Document>
    {
#nullable disable
        private readonly ApplicationDBContext _dbContext;
        public DocumentRepository(ApplicationDBContext dbContext) => _dbContext = dbContext;
        
        public async Task Add(Document entity)
        {
            await _dbContext.Documents.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var documnetToDelete = await _dbContext.Documents.SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.Documents.Remove(documnetToDelete);
        }

        public async Task<IEnumerable<Document>> ReadAll() => await _dbContext.Documents.Include(x => x.Accruals).Include(x => x.Employees).ToListAsync();

        public async Task<Document> ReadById(Guid id) => await _dbContext.Documents.Include(x => x.Accruals).Include(x => x.Employees).SingleOrDefaultAsync(x => x.Id == id);

        public async Task Update(Document entity)
        {
            _dbContext.Documents.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
