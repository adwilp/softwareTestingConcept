using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace VehicleManagement.DBAccess
{
    public interface IVehicleManagementContext
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
            where TEntity : class;

        EntityEntry Update(object entity);

        EntityEntry<TEntity> Update<TEntity>(TEntity entity)
            where TEntity : class;
    }
}
