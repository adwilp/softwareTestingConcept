using System.Linq.Expressions;

using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filterPredicate = null, bool asNoTracking = false, params string[] includedPaths);
        Task<TEntity> GetAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filterPredicate = null, bool asNoTracking = false, params string[] includedPaths);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        void ReloadAllReferenceces(TEntity entity);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
