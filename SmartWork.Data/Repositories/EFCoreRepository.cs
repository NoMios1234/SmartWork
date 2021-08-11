using SmartWork.Core.Abstractions.Repositories;
using SmartWork.Core.Entities;
using SmartWork.Core.Models;
using SmartWork.Core.Specifications;
using SmartWork.Data.AppContext;
using SmartWork.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Data.Repositories
{
    public class EFCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        protected readonly ApplicationContext context;
        protected readonly DbSet<TEntity> entities;
        public EFCoreRepository(ApplicationContext context)
        {
            this.context = context;
            this.entities = this.context.Set<TEntity>();
        }

        public virtual Task AddAsync(TEntity entity)
        {
            this.entities.AddAsync(entity);
            return SaveChangesAsync();
        }
        public virtual Task AddAsync(IEnumerable<TEntity> entities)
        {
            this.entities.AddRangeAsync(entities);
            return SaveChangesAsync();
        }
        public virtual Task<TEntity> FindAsync(int id)
        {
            return this.entities.FirstOrDefaultAsync(x => x.Id == id);
        }
        public virtual Task<TEntity> FindAsync(Specification<TEntity> specification)
        {
            return this.entities.FirstOrDefaultAsync(specification.Expression);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Specification<TEntity> specification)
        {
            return await this.entities.Where(specification.Expression).ToListAsync();
        }
        public virtual Task<PagedList<TEntity>> GetAsync(Specification<TEntity> specification, int pageNumber, int pageSize)
        {
            return this.entities.Where(specification.Expression).ToPagedListAsync(pageNumber, pageSize);
        }
        public virtual Task RemoveAsync(TEntity entities)
        {
            this.entities.Remove(entities);
            return SaveChangesAsync();
        }
        public virtual Task RemoveAsync(IEnumerable<TEntity> entities)
        {
            this.entities.RemoveRange(entities);
            return SaveChangesAsync();
        }   
        public virtual Task UpdateAsync(TEntity entity)
        {
            this.entities.Update(entity);
            return SaveChangesAsync();
        }
        public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            this.entities.UpdateRange(entities);
            return SaveChangesAsync();
        }
        public virtual Task SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }
    }
}