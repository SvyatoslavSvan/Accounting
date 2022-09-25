using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.DAL.Repositories
{
    public class NotBetEmployeeRepository : IBaseEmployeeRepository<NotBetEmployee>
    {
#nullable disable
        private readonly ApplicationDBContext _dbContext;
        public NotBetEmployeeRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(NotBetEmployee entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CountGroupsWithInnerId(string innerId) => await _dbContext.NotBetEmployees.Where(x => x.InnerId == innerId).CountAsync();

        public async Task Delete(Guid id)
        {
            var employeeToDelete = await _dbContext.NotBetEmployees.SingleOrDefaultAsync(x=> x.Id == id);
            _dbContext.NotBetEmployees.Remove(employeeToDelete); 
            await _dbContext.SaveChangesAsync();
        }

        

        public async Task<IEnumerable<NotBetEmployee>> ReadAll() => await _dbContext.NotBetEmployees.Include(x => x.Group).ToListAsync();
        public async Task<NotBetEmployee> ReadById(Guid id) => await _dbContext.NotBetEmployees.Include(x => x.Group).SingleOrDefaultAsync(x => x.Id == id);
        public async Task Update(NotBetEmployee entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
