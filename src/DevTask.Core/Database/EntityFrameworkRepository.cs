using DevTask.Core.Database.Interfaces;
using DevTask.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DevTask.Core.Database
{
    //TODO add all logic for methods
    public class EntityFrameworkRepository<TDbContext, TEntity, TId> : IEntityFrameworkRepoistory<TEntity, TId>
        where TDbContext : DbContext
        where TEntity : Entity<TId>
        where TId : notnull
    {
        private readonly TDbContext _ctx;

        public EntityFrameworkRepository(TDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _ctx.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _ctx.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public Task<TEntity> DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _ctx.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _ctx.Set<TEntity>().Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;

            await _ctx.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
