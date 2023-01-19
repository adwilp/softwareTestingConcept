using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using VehicleManagement.DataContracts.Exceptions;
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

        public TEntity Update(TEntity entity)
        {
            TEntity updatedEntity = Context.Update(entity).Entity;

            return updatedEntity;
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }

        public async Task ReloadReferences(TEntity entity, params string[] properties)
        {
            foreach (string property in properties)
            {
                await Context.Entry(entity).Reference(property).LoadAsync();
            }
        }

        public async Task ReloadAllReferenceces(TEntity entity)
        {
            foreach (var reference in Context.Entry(entity).References)
            {
                if (reference.CurrentValue == null)
                {
                    await reference.LoadAsync();
                }
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException exception)
            {
                throw new SaveDataException(Messages.InvalidData, exception.Entries.Select(e => e.Entity));
            }
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
