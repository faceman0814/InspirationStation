using System.Collections.Concurrent;
using System.Linq.Expressions;
using FaceMan.Utils.Dependency;
using FaceMan.Utils.Entities;
using FaceMan.Utils.Exception;

namespace FaceMan.Utils;

public abstract class RepositoryBase<TDbContext, TEntity, TPrimaryKey> : ITransientDependency
    where TEntity : class, IEntity<TPrimaryKey>
{
    public static readonly ConcurrentDictionary<Type, bool> EntityIsDbQuery = new ConcurrentDictionary<Type, bool>();
    public abstract IQueryable<TEntity> GetAll();

    public abstract Task<IQueryable<TEntity>> GetAllAsync();

    public virtual IQueryable<TEntity> GetAllIncluding(
        params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        return this.GetAll();
    }

    public virtual Task<IQueryable<TEntity>> GetAllIncludingAsync(
        params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        return this.GetAllAsync();
    }

    public virtual List<TEntity> GetAllList() => this.GetAll().ToList<TEntity>();

    public virtual Task<List<TEntity>> GetAllListAsync()
    {
        return Task.FromResult<List<TEntity>>(this.GetAllList());
    }

    public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
    {
        return this.GetAll().Where<TEntity>(predicate).ToList<TEntity>();
    }

    public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Task.FromResult<List<TEntity>>(this.GetAllList(predicate));
    }

    public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
    {
        return queryMethod(this.GetAll());
    }

    public virtual TEntity Get(TPrimaryKey id)
    {
        return this.FirstOrDefault(id) ?? throw new EntityNotFoundException(typeof(TEntity), (object)id);
    }

    public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
    {
        return await this.FirstOrDefaultAsync(id).ConfigureAwait(false) ??
               throw new EntityNotFoundException(typeof(TEntity), (object)id);
    }

    public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
    {
        return this.GetAll().Single<TEntity>(predicate);
    }

    public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Task.FromResult<TEntity>(this.Single(predicate));
    }

    public virtual TEntity FirstOrDefault(TPrimaryKey id)
    {
        return this.GetAll().FirstOrDefault<TEntity>(this.CreateEqualityExpressionForId(id));
    }

    public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
    {
        return Task.FromResult<TEntity>(this.FirstOrDefault(id));
    }

    public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return this.GetAll().FirstOrDefault<TEntity>(predicate);
    }

    public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Task.FromResult<TEntity>(this.FirstOrDefault(predicate));
    }

    public virtual TEntity Load(TPrimaryKey id) => this.Get(id);

    public abstract TEntity Insert(TEntity entity);

    public virtual Task<TEntity> InsertAsync(TEntity entity)
    {
        return Task.FromResult<TEntity>(this.Insert(entity));
    }

    public virtual TPrimaryKey InsertAndGetId(TEntity entity) => this.Insert(entity).Id;

    public virtual async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        return (await this.InsertAsync(entity).ConfigureAwait(false)).Id;
    }

    public virtual TEntity InsertOrUpdate(TEntity entity)
    {
        return !entity.IsTransient() ? this.Update(entity) : this.Insert(entity);
    }

    public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
    {
        TEntity entity1;
        if (entity.IsTransient())
            entity1 = await this.InsertAsync(entity).ConfigureAwait(false);
        else
            entity1 = await this.UpdateAsync(entity).ConfigureAwait(false);
        return entity1;
    }

    public virtual TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
    {
        return this.InsertOrUpdate(entity).Id;
    }

    public virtual async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
    {
        return (await this.InsertOrUpdateAsync(entity).ConfigureAwait(false)).Id;
    }

    public abstract TEntity Update(TEntity entity);

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        return Task.FromResult<TEntity>(this.Update(entity));
    }

    public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
    {
        TEntity entity = this.Get(id);
        updateAction(entity);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
    {
        TEntity entity = await this.GetAsync(id).ConfigureAwait(false);
        await updateAction(entity).ConfigureAwait(false);
        TEntity entity1 = entity;
        entity = default(TEntity);
        return entity1;
    }

    public abstract void Delete(TEntity entity);

    public virtual Task DeleteAsync(TEntity entity)
    {
        this.Delete(entity);
        return Task.CompletedTask;
    }

    public abstract void Delete(TPrimaryKey id);

    public virtual Task DeleteAsync(TPrimaryKey id)
    {
        this.Delete(id);
        return Task.CompletedTask;
    }

    public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        foreach (TEntity all in this.GetAllList(predicate))
            this.Delete(all);
    }

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        foreach (TEntity entity in await this.GetAllListAsync(predicate).ConfigureAwait(false))
            await this.DeleteAsync(entity).ConfigureAwait(false);
    }

    public virtual int Count() => this.GetAll().Count<TEntity>();

    public virtual Task<int> CountAsync() => Task.FromResult<int>(this.Count());

    public virtual int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return this.GetAll().Count<TEntity>(predicate);
    }

    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Task.FromResult<int>(this.Count(predicate));
    }

    public virtual long LongCount() => this.GetAll().LongCount<TEntity>();

    public virtual Task<long> LongCountAsync() => Task.FromResult<long>(this.LongCount());

    public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
    {
        return this.GetAll().LongCount<TEntity>(predicate);
    }

    public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Task.FromResult<long>(this.LongCount(predicate));
    }

    protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    {
        ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity));
        MemberExpression left = Expression.PropertyOrField((Expression)parameterExpression, "Id");
        object idValue = Convert.ChangeType((object)id, typeof(TPrimaryKey));
        UnaryExpression right = Expression.Convert(((LambdaExpression)(() => idValue)).Body, left.Type);
        return Expression.Lambda<Func<TEntity, bool>>((Expression)Expression.Equal((Expression)left, (Expression)right),
            parameterExpression);
    }
}