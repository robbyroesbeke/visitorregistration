// (c) Visitor Registration

using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;

namespace VisitorRegistration.BE.Infrastructure.Persistence.EF.Repositories;

internal class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    public BaseRepository (
        VisitorRegistrationDbContext visitorRegistrationDbContext)
    {
        VisitorRegistrationDbContext = visitorRegistrationDbContext;
    }

    protected VisitorRegistrationDbContext VisitorRegistrationDbContext { get; }

    public async ValueTask<T?> GetByIdAsync
        (int id, CancellationToken cancellationToken)
        => await VisitorRegistrationDbContext
            .Set<T>()
            .FindAsync(new object[] { id }, cancellationToken);

    public async ValueTask<IList<T>> GetAllAsync (int page, int pageSize, CancellationToken cancellationToken)
        => await GetAllAsync(page, pageSize, null, cancellationToken);

    public virtual async ValueTask<IList<T>> GetAllAsync (
        int page,
        int pageSize,
        Expression<Func<T, bool>>? predicate,
        CancellationToken cancellationToken)
    {
        IQueryable<T> collection = VisitorRegistrationDbContext.Set<T>().AsQueryable();

        if ( predicate != null )
        {
            collection = collection.Where(predicate);
        }

        return await collection.Skip(( page - 1 ) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async ValueTask<T> AddAsync (T entity, CancellationToken cancellation)
    {
        _ = await VisitorRegistrationDbContext.Set<T>().AddAsync(entity, cancellation);
        _ = await VisitorRegistrationDbContext.SaveChangesAsync(cancellation);

        return entity;
    }

    public async ValueTask UpdateAsync (T entity, CancellationToken cancellation)
    {
        VisitorRegistrationDbContext.Entry(entity).State = EntityState.Modified;
        _ = await VisitorRegistrationDbContext.SaveChangesAsync(cancellation);
    }

    public async ValueTask DeleteAsync (T entity, CancellationToken cancellation)
    {
        _ = VisitorRegistrationDbContext.Set<T>().Remove(entity);
        _ = await VisitorRegistrationDbContext.SaveChangesAsync(cancellation);
    }
}