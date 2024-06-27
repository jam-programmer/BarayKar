using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Configuration.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EfRepository<TEntity> : IEfRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SqlServerContext _context;

        public EfRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await SaveChangeAsync();
        }
        public async Task DeleteListAsync(List<TEntity> items)
        {
            _context.Set<TEntity>().RemoveRange(items);
            await SaveChangeAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellation)
        {
            return (await _context.Set<TEntity>().FirstOrDefaultAsync(cancellation))!;
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            return (await _context.Set<TEntity>().AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id, cancellation))!;
        }

        public Task<IQueryable<TEntity>> GetByQueryAsync()
        {
            return Task.FromResult(_context.Set<TEntity>().AsQueryable());
        }

        public async Task<TEntity> GetDeletedByIdAsync(Guid id, CancellationToken cancellation)
        {
            return (await _context.Set<TEntity>().IgnoreQueryFilters().
                SingleOrDefaultAsync(s => s.Id == id, cancellation))!;
        }

        public async Task<ICollection<TEntity>> GetListAsync(CancellationToken cancellation)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellation);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellation)
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellation);
            await SaveChangeAsync();
        }

        public async Task InsertListAsync(List<TEntity> items, CancellationToken cancellation)
        {
            await _context.AddRangeAsync(items, cancellation);
            await SaveChangeAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }



        public async Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await SaveChangeAsync();
        }



    }
}
