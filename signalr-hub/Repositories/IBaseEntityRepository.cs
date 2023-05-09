using signalr_hub.Entities;
using System.Linq.Expressions;

namespace signalr_hub.Repositories;

public interface IBaseEntityRepository<TEntity> : IDisposable
    where TEntity : Entity, new()
{
    Task<TEntity> CreateAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> GetFirstByIdAsync(Guid id);

    Task DeleteAsync(TEntity entity);
}
