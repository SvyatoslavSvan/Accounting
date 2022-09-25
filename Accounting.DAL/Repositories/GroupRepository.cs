using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.DAL.Repositories
{
#nullable disable
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public GroupRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Group entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CountGroupsWithName(string name) => await _dbContext.Groups.Where(x => x.Name == name).CountAsync();

        public async Task Delete(Guid id)
        {
            var groupToDelete = await _dbContext.Groups.SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.Groups.Remove(groupToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> ReadAll() => await _dbContext.Groups.Include(x => x.BetEmployees).Include(x => x.NotBetEmployees).ToListAsync();
        public async Task<Group> ReadById(Guid id) => await _dbContext.Groups.Include(x => x.BetEmployees).Include(x => x.NotBetEmployees).SingleOrDefaultAsync(x => x.Id == id);
        public async Task Update(Group entity)
        {
            _dbContext.Groups.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
