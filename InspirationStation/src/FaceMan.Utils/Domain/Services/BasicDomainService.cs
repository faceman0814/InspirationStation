using System.Linq.Expressions;
using FaceMan.Utils.Domain.Repositories;
using FaceMan.Utils.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FaceMan.Utils.Domain.Services;

public class BasicDomainService<TEntity, TPrimaryKey> : 
    DomainService,
    IBasicDomainService< TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
     public BasicDomainService(IServiceProvider serviceProvider, string localizationSourceName = null)
    {
      this.ServiceProvider = serviceProvider;
      this.EntityRepo = serviceProvider.GetRequiredService<IRepository<TEntity, TPrimaryKey>>();
      this.LocalizationSourceName = localizationSourceName ?? AppConsts.LocalizationSourceName;
    }

    public virtual IServiceProvider ServiceProvider { get; }

    public virtual IRepository<TEntity, TPrimaryKey> EntityRepo { get; }

    public virtual IQueryable<TEntity> Query => this.EntityRepo.GetAll();

    public virtual IQueryable<TEntity> QueryAsNoTracking => this.Query.AsNoTracking<TEntity>();

    public virtual async Task<TEntity> FindByIdAsync(TPrimaryKey id)
    {
      return await this.EntityRepo.FirstOrDefaultAsync(id);
    }

    public virtual async Task Create(TEntity entity, bool createAndGetId = false)
    {
      if (createAndGetId)
      {
        TPrimaryKey idAsync = await this.EntityRepo.InsertAndGetIdAsync(entity);
      }
      else
      {
        TEntity entity1 = await this.EntityRepo.InsertAsync(entity);
      }
    }

    public async Task Create(IEnumerable<TEntity> entities, bool createAndGetId = false)
    {
      foreach (TEntity entity in entities)
        await this.Create(entity, createAndGetId);
    }

    public virtual async Task Update(TEntity entity)
    {
      TEntity entity1 = await this.EntityRepo.UpdateAsync(entity);
    }

    public virtual async Task Update(IEnumerable<TEntity> entities)
    {
      foreach (TEntity entity in entities)
        await this.Update(entity);
    }

    public virtual async Task Delete(TPrimaryKey id) => await this.EntityRepo.DeleteAsync(id);

    public virtual async Task Delete(TEntity entity) => await this.EntityRepo.DeleteAsync(entity);

    public virtual async Task Delete(List<TPrimaryKey> idList)
    {
      if (idList == null || idList.Count == 0)
        return;
      await this.EntityRepo.DeleteAsync((Expression<Func<TEntity, bool>>) (o => idList.Contains(o.Id)));
    }

    public async Task Delete(Expression<Func<TEntity, bool>> predicate)
    {
      await this.EntityRepo.DeleteAsync(predicate);
    }

    public async Task<bool> Exist(TPrimaryKey id)
    {
      return await this.EntityRepo.CountAsync((Expression<Func<TEntity, bool>>) (o => o.Id.Equals((object) id))) > 0;
    }

    public async Task<TEntity> CreateOrUpdate(TEntity entity)
    {
      return await this.EntityRepo.InsertOrUpdateAsync(entity);
    }

    public async Task CreateOrUpdate(IEnumerable<TEntity> entities)
    {
      foreach (TEntity entity1 in entities)
      {
        TEntity entity2 = await this.EntityRepo.InsertOrUpdateAsync(entity1);
      }
    }

    /// <summary>获取服务实例</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetService<T>() => this.ServiceProvider.GetRequiredService<T>();
}