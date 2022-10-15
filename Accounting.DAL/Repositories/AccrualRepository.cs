﻿using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.DAL.Repositories
{
    public class AccrualRepository : IAccrualRepository
    {
#nullable disable
        private readonly ApplicationDBContext _dbContext;
        public AccrualRepository(ApplicationDBContext dbContext) => _dbContext = dbContext;

        public async Task Add(Accrual entity)
        {
            _dbContext.Attach(entity.Employee);
            await _dbContext.Accruals.AddAsync(entity);
            await _dbContext.SaveChangesAsync();   
        }

        public async Task Delete(Guid id)
        {
            var accrualToDelete = await _dbContext.Accruals.SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.Accruals.Remove(accrualToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Accrual> accruals)
        {
            _dbContext.Accruals.RemoveRange(accruals);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeByDocumentId(Guid documentId)
        {
            var accrualsToDelete = await _dbContext.Accruals.Include(x => x.Document).Where(x => x.Document.Id == documentId).ToListAsync();
            _dbContext.Accruals.RemoveRange(accrualsToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Accrual>> ReadAll() => await _dbContext.Accruals.Include(x => x.Employee).ToListAsync();

        public async Task<Accrual> ReadById(Guid id) => await _dbContext.Accruals.Include(x => x.Employee).SingleOrDefaultAsync(x => x.Id == id);

        public async Task Update(Accrual entity)
        {
            _dbContext.Accruals.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
