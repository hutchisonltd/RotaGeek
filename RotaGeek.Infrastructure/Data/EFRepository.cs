using Microsoft.EntityFrameworkCore;
using RotaGeek.Core.Interfaces;
using RotaGeek.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RotaGeek.Infrastructure.Data
{
    public class EFRepository : IRepository
    {
        private readonly AppDbContext _dbContext;

        public EFRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> List<T>(CancellationToken ct = default(CancellationToken)) where T : BaseEntity
        {
            ct.ThrowIfCancellationRequested();
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Get<T>(int id, CancellationToken ct = default(CancellationToken)) where T : BaseEntity
        {
            ct.ThrowIfCancellationRequested();
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public T Add<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }
    }
}
