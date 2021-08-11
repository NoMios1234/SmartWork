using SmartWork.Core.Entities;
using SmartWork.Core.Models;
using SmartWork.Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWork.Core.Abstractions.Repositories
{
    public interface IRepository<TEntity>
               where TEntity : Entity
    {
        Task<TEntity> FindAsync(int id);
        Task<TEntity> FindAsync(Specification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetAsync(Specification<TEntity> specification);
        Task<PagedList<TEntity>> GetAsync(Specification<TEntity> specification, int pageNumber, int pageSize);
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entity);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IEnumerable<TEntity> entity);
        Task RemoveAsync(TEntity entity);
        Task RemoveAsync(IEnumerable<TEntity> entity);
        Task SaveChangesAsync();
    }
}
