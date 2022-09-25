using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.DAL.Repositories
{
#nullable disable
    public class BetEmployeeRepository : IBaseEmployeeRepository<BetEmployee>
    {
        private readonly ApplicationDBContext _dbContext;
        public BetEmployeeRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(BetEmployee entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CountGroupsWithInnerId(string innerId) => await _dbContext.BetEmployees.Where(x => x.InnerId == innerId).CountAsync();

        public async Task Delete(Guid id)
        {
            var employeeToDelete = await _dbContext.BetEmployees.SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.BetEmployees.Remove(employeeToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BetEmployee>> ReadAll() => await _dbContext.BetEmployees.Include(x => x.Group).ToListAsync();

        public async Task<BetEmployee> ReadById(Guid id) => await _dbContext.BetEmployees.Include(x => x.Group).SingleOrDefaultAsync(x => x.Id == id);

        public async Task Update(BetEmployee entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
