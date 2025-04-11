using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Template.Domain.Primitives;

namespace Template.Infrastructure.Implementations;

public class Repository<TEntity> where TEntity : Entity
{
    protected readonly AppDbContext appDbContext;

    public Repository(AppDbContext context)
    {
        appDbContext = context;
    }

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await appDbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Set<TEntity>().FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression, CancellationToken cancellationToken = default)
    {
        var query = appDbContext.Set<TEntity>().AsQueryable();

        if (expression != null)
        {
            query = query.Where(expression);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await appDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            appDbContext.Set<TEntity>().Update(entity);
        }, cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            appDbContext.Set<TEntity>().Remove(entity);
        }, cancellationToken);
    }

    public async Task<bool> Exists(Expression<Func<TEntity, bool>> expression)
    {
        return await Task.FromResult(appDbContext.Set<TEntity>().Any(expression));
    }
}
