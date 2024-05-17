using Application.Abstractions.Interfaces;
using Domain.Common;

namespace Infrastructure.Services;

public class Repository<T>(ApplicationDbContext context) : IRepository<T>
    where T : BaseEntity
{
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await context.AddAsync(entity, cancellationToken);
    }

    public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<ICollection<T>> GetAsync(CancellationToken cancellationToken)
    {
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public IQueryable<T> GetQueryable()
    {
        return context.Set<T>().AsQueryable();
    }
}