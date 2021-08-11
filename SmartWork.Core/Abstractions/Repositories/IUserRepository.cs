using Microsoft.AspNetCore.Identity;
using SmartWork.Core.Models;
using SmartWork.Core.Specifications.UserSpecification;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartWork.Core.Abstractions.Repositories
{
    public interface IUserRepository<TEntity>
           where TEntity : IdentityUser
    {
        Task<TEntity> FindAsync(string id, CancellationToken cancellationToken = default);
        Task<TEntity> FindAsync(UserSpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(UserSpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<PagedList<TEntity>> GetAsync(UserSpecification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IEnumerable<TEntity> entity);
        Task RemoveAsync(TEntity entity);
        Task RemoveAsync(IEnumerable<TEntity> entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
