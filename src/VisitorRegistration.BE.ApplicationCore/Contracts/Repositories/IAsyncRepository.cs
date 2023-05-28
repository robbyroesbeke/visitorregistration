// (c) Visitor Registration

using System.Linq.Expressions;

namespace VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;

public interface IAsyncRepository<T>
    where T : class
{
    ValueTask<T?> GetByIdAsync (int id, CancellationToken cancellationToken);

    ValueTask<IList<T>> GetAllAsync (int page, int pageSize, CancellationToken cancellationToken);

    ValueTask<IList<T>> GetAllAsync (int page, int pageSize, Expression<Func<T, bool>>? predicate,
        CancellationToken cancellationToken);

    ValueTask<T> AddAsync (T entity, CancellationToken cancellation);

    ValueTask UpdateAsync (T entity, CancellationToken cancellation);

    ValueTask DeleteAsync (T entity, CancellationToken cancellation);
}