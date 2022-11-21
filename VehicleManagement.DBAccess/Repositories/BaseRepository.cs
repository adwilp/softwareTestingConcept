using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IVehicleManagementContext _context;

        protected IVehicleManagementContext Context
        {
            get { return _context; }
        }

        public BaseRepository(IVehicleManagementContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filterPredicate = null, bool asNoTracking = false, params string[] includedPaths)
        {
            var query = GetAllAsQueryable(filterPredicate, asNoTracking, includedPaths);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filterPredicate = null, bool asNoTracking = false, params string[] includedPaths)
        {
            var dbSet = Context.Set<TEntity>();
            IQueryable<TEntity> query = dbSet;

            if (asNoTracking)
            {
                query = dbSet.AsNoTracking();
            }

            if (includedPaths != null)
            {
                foreach (var includedPath in includedPaths)
                {
                    query = query.Include(includedPath);
                }
            }

            if (filterPredicate != null)
            {
                query = query.Where(filterPredicate);
            }

            return await query.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            TEntity createdEntity = (await Context.Set<TEntity>().AddAsync(entity, cancellationToken)).Entity;

            return createdEntity;
        }

        /// <summary>
        /// Reloads all referenceces.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void ReloadAllReferenceces(TEntity entity)
        {
            foreach (var reference in Context.Entry(entity).References)
            {
                if (reference.CurrentValue == null)
                {
                    reference.Load();
                }
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        protected IQueryable<TEntity> GetAllAsQueryable(Expression<Func<TEntity, bool>> filterPredicate = null, bool asNoTracking = false, params string[] includedPaths)
        {
            var dbSet = Context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            if (includedPaths != null)
            {
                foreach (var includedPath in includedPaths)
                {
                    query = query.Include(includedPath);
                }
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (filterPredicate != null)
            {
                query = query.Where(filterPredicate);
            }

            return query;
        }
    }
}
