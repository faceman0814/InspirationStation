using FaceMan.Utils.Entities;
using FaceMan.Utils.Helper;
using Microsoft.EntityFrameworkCore;

namespace FaceMan.Utils.Domain.Repositories;

public static class EfCoreRepositoryExtensions
{
    public static DbContext GetDbContext<TEntity, TPrimaryKey>(
        this IRepository<TEntity, TPrimaryKey> repository)
        where TEntity : class, IEntity<TPrimaryKey>
    {
        return ProxyHelper.UnProxy((object)repository) is IRepositoryWithDbContext repositoryWithDbContext
            ? repositoryWithDbContext.GetDbContext()
            : throw new ArgumentException("Given repository does not implement IRepositoryWithDbContext",
                nameof(repository));
    }

    public static void DetachFromDbContext<TEntity, TPrimaryKey>(
        this IRepository<TEntity, TPrimaryKey> repository,
        TEntity entity)
        where TEntity : class, IEntity<TPrimaryKey>
    {
        repository.GetDbContext<TEntity, TPrimaryKey>().Entry<TEntity>(entity).State = EntityState.Detached;
    }
}