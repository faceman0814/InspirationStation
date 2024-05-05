using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using EntityFramework.DbContext;
using FaceMan.Utils.Entities;
using FaceManUtils.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EntityFramework.Repository;

public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IFullAuditedEntity<TPrimaryKey>
{
    private readonly InspirationStationDbContext _context;

    public Repository(InspirationStationDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> FindByIdAsync(string id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task CreateAsync(TEntity entity)
    {
        // entity.Id = Guid.NewGuid().ToString("N");
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetAll()
    {
        //实现GetAll方法
        return _context.Set<TEntity>();
    }



    public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[]? propertySelectors)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (propertySelectors == null || propertySelectors.Length == 0)
        {
            // 如果没有提供任何选择器，则尝试包含所有导航属性
            var navigations = _context.Model.FindEntityType(typeof(TEntity))
                .GetNavigations()
                .Select(e => e.Name);
        
            foreach (string navigationName in navigations)
            {
                query = query.Include(navigationName);
            }
        }
        else
        {
            foreach (var propertySelector in propertySelectors)
            {
                // 递归地包括所有级别的导航属性
                query = IncludeProperty(query, propertySelector.Body);
            }
        }
        return query;
    }

    private IQueryable<TEntity> IncludeProperty(IQueryable<TEntity> query, Expression propertyExpression)
    {
        // 使用反射从 Entity Framework 中获取 Include 方法的引用
        var includeMethod = typeof(EntityFrameworkQueryableExtensions)
            .GetMethods()
            .First(method => method.Name == "Include"
                             && method.IsGenericMethodDefinition
                             && method.GetParameters().Length == 2);

        if (propertyExpression is MemberExpression memberExpression)
        {
            // 如果成员是另一个实体，则添加Include
            var entityType = memberExpression.Expression.Type;
            var propertyType = ((PropertyInfo)memberExpression.Member).PropertyType;

            var genericIncludeMethod = includeMethod.MakeGenericMethod(entityType, propertyType);
            query = (IQueryable<TEntity>)genericIncludeMethod.Invoke(null, new object[] { query, propertyExpression });

            // 检查是否有嵌套的ThenInclude
            if (propertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(propertyType))
            {
                // 实体集合类型，我们需要处理集合中的每个元素
                var collectionItemType = propertyType.GetGenericArguments()[0];
                var includeProperties = collectionItemType.GetProperties()
                    .Where(p => p.PropertyType.Namespace == "YourEntityNamespace").ToList();

                foreach (var includeProperty in includeProperties)
                {
                    // 构建新的表达式，用于后续的ThenInclude调用
                    var parameter = Expression.Parameter(collectionItemType, "i");
                    var property = Expression.Property(parameter, includeProperty);
                    var lambda = Expression.Lambda(property, parameter);

                    // 递归调用
                    query = IncludeProperty(query, lambda);
                }
            }
        }
        else if (propertyExpression is NewExpression newExpression)
        {
            // 处理匿名类型（例如 .Select(x => new { x.Navigation1, x.Navigation2 })）
            foreach (var argument in newExpression.Arguments)
            {
                query = IncludeProperty(query, argument);
            }
        }

        return query;
    }

    public List<TEntity> GetAllList()
    {
        throw new NotImplementedException();
    }

    public async Task<List<TEntity>> GetAllListAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
    {
        throw new NotImplementedException();
    }

    public TEntity Get(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetAsync(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public TEntity Single(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public TEntity FirstOrDefault(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public TEntity Load(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public TEntity Insert(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public TPrimaryKey InsertAndGetId(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity InsertOrUpdate(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> InsertOrUpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    Task<TEntity> IRepository<TEntity, TPrimaryKey>.UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        entity.LastModificationTime = DateTime.Now;
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public void Delete(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public long LongCount()
    {
        throw new NotImplementedException();
    }

    public Task<long> LongCountAsync()
    {
        throw new NotImplementedException();
    }

    public long LongCount(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}