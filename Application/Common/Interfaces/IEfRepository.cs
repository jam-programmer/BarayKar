using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEfRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task DeleteListAsync(List<TEntity> items);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ICollection<TEntity>> GetListAsync(CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetDeletedByIdAsync(Guid id, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<TEntity>FirstOrDefaultAsync(CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetByQueryAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(TEntity entity, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task InsertListAsync(List<TEntity> items, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task SaveChangeAsync();

    }
}
