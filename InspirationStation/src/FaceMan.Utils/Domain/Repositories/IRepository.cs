﻿using System.Linq.Expressions;
using FaceMan.Utils.Domain.Services;
using FaceMan.Utils.Entities;

namespace FaceMan.Utils.Domain.Repositories;

public interface IRepository<TEntity, TPrimaryKey> :  ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
{
     /// <summary>
    /// 用于获取用于从整个表中检索实体的 IQueryable。
    /// </summary>
    /// <returns>IQueryable to be used to select entities from database</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// 用于获取用于从整个表中检索实体的 IQueryable。
    /// One or more
    /// </summary>
    /// <param name="propertySelectors">包含表达式的列表。</param>
    /// <returns>IQueryable 用于从数据库中选择实体</returns>
    IQueryable<TEntity> GetAllIncluding(
      params Expression<Func<TEntity, object>>[] propertySelectors);

    /// <summary>用于获取所有实体。</summary>
    /// <returns>所有实体的列表</returns>
    List<TEntity> GetAllList();

    /// <summary>用于获取所有实体.</summary>
    /// <returns>所有实体的列表</returns>
    Task<List<TEntity>> GetAllListAsync();

    /// <summary>
    /// 用于获取基于给定的所有实体 <paramref name="predicate" />.
    /// </summary>
    /// <param name="predicate">筛选实体的条件</param>
    /// <returns>所有实体的列表</returns>
    List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Used to get all entities based on given <paramref name="predicate" />.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    /// <returns>List of all entities</returns>
    Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Used to run a query over entire entities.
    /// <see cref="T:Abp.Domain.Uow.UnitOfWorkAttribute" /> attribute is not always necessary (as opposite to <see cref="M:Abp.Domain.Repositories.IRepository`2.GetAll" />)
    /// if <paramref name="queryMethod" /> finishes IQueryable with ToList, FirstOrDefault etc..
    /// </summary>
    /// <typeparam name="T">Type of return value of this method</typeparam>
    /// <param name="queryMethod">This method is used to query over entities</param>
    /// <returns>Query result</returns>
    T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

    /// <summary>Gets an entity with given primary key.</summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity</returns>
    TEntity Get(TPrimaryKey id);

    /// <summary>Gets an entity with given primary key.</summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity</returns>
    Task<TEntity> GetAsync(TPrimaryKey id);

    /// <summary>
    /// Gets exactly one entity with given predicate.
    /// Throws exception if no entity or more than one entity.
    /// </summary>
    /// <param name="predicate">Entity</param>
    TEntity Single(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets exactly one entity with given predicate.
    /// Throws exception if no entity or more than one entity.
    /// </summary>
    /// <param name="predicate">Entity</param>
    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets an entity with given primary key or null if not found.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity or null</returns>
    TEntity FirstOrDefault(TPrimaryKey id);

    /// <summary>
    /// Gets an entity with given primary key or null if not found.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity or null</returns>
    Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

    /// <summary>
    /// Gets an entity with given given predicate or null if not found.
    /// </summary>
    /// <param name="predicate">Predicate to filter entities</param>
    TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets an entity with given given predicate or null if not found.
    /// </summary>
    /// <param name="predicate">Predicate to filter entities</param>
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Creates an entity with given primary key without database access.
    /// </summary>
    /// <param name="id">Primary key of the entity to load</param>
    /// <returns>Entity</returns>
    TEntity Load(TPrimaryKey id);

    /// <summary>Inserts a new entity.</summary>
    /// <param name="entity">Inserted entity</param>
    TEntity Insert(TEntity entity);

    /// <summary>Inserts a new entity.</summary>
    /// <param name="entity">Inserted entity</param>
    Task<TEntity> InsertAsync(TEntity entity);

    /// <summary>
    /// Inserts a new entity and gets it's Id.
    /// It may require to save current unit of work
    /// to be able to retrieve id.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Id of the entity</returns>
    TPrimaryKey InsertAndGetId(TEntity entity);

    /// <summary>
    /// Inserts a new entity and gets it's Id.
    /// It may require to save current unit of work
    /// to be able to retrieve id.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Id of the entity</returns>
    Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

    /// <summary>
    /// Inserts or updates given entity depending on Id's value.
    /// </summary>
    /// <param name="entity">Entity</param>
    TEntity InsertOrUpdate(TEntity entity);

    /// <summary>
    /// Inserts or updates given entity depending on Id's value.
    /// </summary>
    /// <param name="entity">Entity</param>
    Task<TEntity> InsertOrUpdateAsync(TEntity entity);

    /// <summary>
    /// Inserts or updates given entity depending on Id's value.
    /// Also returns Id of the entity.
    /// It may require to save current unit of work
    /// to be able to retrieve id.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Id of the entity</returns>
    TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);

    /// <summary>
    /// Inserts or updates given entity depending on Id's value.
    /// Also returns Id of the entity.
    /// It may require to save current unit of work
    /// to be able to retrieve id.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Id of the entity</returns>
    Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity);

    /// <summary>Updates an existing entity.</summary>
    /// <param name="entity">Entity</param>
    TEntity Update(TEntity entity);

    /// <summary>Updates an existing entity.</summary>
    /// <param name="entity">Entity</param>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>Updates an existing entity.</summary>
    /// <param name="id">Id of the entity</param>
    /// <param name="updateAction">Action that can be used to change values of the entity</param>
    /// <returns>Updated entity</returns>
    TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

    /// <summary>Updates an existing entity.</summary>
    /// <param name="id">Id of the entity</param>
    /// <param name="updateAction">Action that can be used to change values of the entity</param>
    /// <returns>Updated entity</returns>
    Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

    /// <summary>Deletes an entity.</summary>
    /// <param name="entity">Entity to be deleted</param>
    void Delete(TEntity entity);

    /// <summary>Deletes an entity.</summary>
    /// <param name="entity">Entity to be deleted</param>
    Task DeleteAsync(TEntity entity);

    /// <summary>Deletes an entity by primary key.</summary>
    /// <param name="id">Primary key of the entity</param>
    void Delete(TPrimaryKey id);

    /// <summary>Deletes an entity by primary key.</summary>
    /// <param name="id">Primary key of the entity</param>
    Task DeleteAsync(TPrimaryKey id);

    /// <summary>
    /// Deletes many entities by function.
    /// Notice that: All entities fits to given predicate are retrieved and deleted.
    /// This may cause major performance problems if there are too many entities with
    /// given predicate.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    void Delete(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Deletes many entities by function.
    /// Notice that: All entities fits to given predicate are retrieved and deleted.
    /// This may cause major performance problems if there are too many entities with
    /// given predicate.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>Gets count of all entities in this repository.</summary>
    /// <returns>Count of entities</returns>
    int Count();

    /// <summary>Gets count of all entities in this repository.</summary>
    /// <returns>Count of entities</returns>
    Task<int> CountAsync();

    /// <summary>
    /// Gets count of all entities in this repository based on given <paramref name="predicate" />.
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <returns>Count of entities</returns>
    int Count(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets count of all entities in this repository based on given <paramref name="predicate" />.
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <returns>Count of entities</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets count of all entities in this repository (use if expected return value is greater than <see cref="F:System.Int32.MaxValue" />.
    /// </summary>
    /// <returns>Count of entities</returns>
    long LongCount();

    /// <summary>
    /// Gets count of all entities in this repository (use if expected return value is greater than <see cref="F:System.Int32.MaxValue" />.
    /// </summary>
    /// <returns>Count of entities</returns>
    Task<long> LongCountAsync();

    /// <summary>
    /// Gets count of all entities in this repository based on given <paramref name="predicate" />
    /// (use this overload if expected return value is greater than <see cref="F:System.Int32.MaxValue" />).
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <returns>Count of entities</returns>
    long LongCount(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 根据给定的 <paramref name="predicate" />
    /// （如果预期返回值大于 <see cref="F:System.Int32.MaxValue" />).
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <returns>Count of entities</returns>
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
}