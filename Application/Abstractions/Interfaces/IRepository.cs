using Domain.Common;

namespace Application.Abstractions.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity, CancellationToken cancellationToken);

    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<ICollection<T>> GetAsync(CancellationToken cancellationToken);

    void Delete(T entity);

    void Update(T entity);

    IQueryable<T> GetQueryable();
}