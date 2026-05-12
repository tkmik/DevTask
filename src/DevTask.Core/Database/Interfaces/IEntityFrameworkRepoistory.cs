namespace DevTask.Core.Database.Interfaces
{
    public interface IEntityFrameworkRepoistory<TEntity, TId>
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefaultAsync(TId id, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> DeleteAsync(TId id, CancellationToken cancellationToken = default);
    }
}
