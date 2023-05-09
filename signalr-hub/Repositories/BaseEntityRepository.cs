using Microsoft.EntityFrameworkCore;
using signalr_hub.Context;
using signalr_hub.Entities;
using System.Linq.Expressions;

namespace signalr_hub.Repositories;

public class BaseEntityRepository<TEntity> : IBaseEntityRepository<TEntity>
    where TEntity : Entity, new()
{
    protected readonly SignalRDataContext _context;

    protected DbSet<TEntity> _dbSet;

    public BaseEntityRepository(SignalRDataContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _dbSet
                    .AsNoTracking()
                    .ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbSet
                    .AsNoTracking()
                    .Where(predicate)
                    .ToListAsync();

    public async Task<TEntity> GetFirstByIdAsync(Guid id)
        => await _dbSet
                    .AsNoTracking()
                    .FirstAsync(w => w.Id == id);

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
