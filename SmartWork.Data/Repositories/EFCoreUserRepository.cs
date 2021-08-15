using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Abstractions.Repositories;
using SmartWork.Core.Models;
using SmartWork.Core.Specifications.UserSpecification;
using SmartWork.Data.AppContext;
using SmartWork.Data.Extentions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartWork.Data.Repositories
{
    public class EFCoreUserRepository<TEntity> : IUserRepository<TEntity>
        where TEntity : IdentityUser
    {
        protected readonly ApplicationContext context;
        protected readonly DbSet<TEntity> entities;
        public EFCoreUserRepository(ApplicationContext context)
        {
            this.context = context;
            this.entities = this.context.Set<TEntity>();
        }

        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return this.entities.AddAsync(entity, cancellationToken).AsTask();
        }
        public virtual Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return this.entities.AddRangeAsync(entities, cancellationToken);
        }
        public virtual Task<TEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            return this.entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public virtual Task<TEntity> FindAsync(UserSpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return this.entities.FirstOrDefaultAsync(specification.Expression, cancellationToken);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync(UserSpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await this.entities.Where(specification.Expression).ToListAsync(cancellationToken).ConfigureAwait(false);
        }
        public virtual Task<PagedList<TEntity>> GetAsync(UserSpecification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return this.entities.Where(specification.Expression).ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }
        public virtual Task RemoveAsync(TEntity entities)
        {
            this.entities.Remove(entities);
            return Task.CompletedTask;
        }
        public virtual Task RemoveAsync(IEnumerable<TEntity> entities)
        {
            this.entities.RemoveRange(entities);
            return Task.CompletedTask;
        }
        public virtual Task UpdateAsync(TEntity entity)
        {
            this.entities.Update(entity);
            return Task.CompletedTask;
        }
        public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            this.entities.UpdateRange(entities);
            return Task.CompletedTask;
        }
        public virtual Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
