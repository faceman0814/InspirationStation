using EntityFramework.Repository;
using FaceMan.Utils.Dependency;
using FaceMan.Utils.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.DomainService;

public class DomainService<TEntity, TPrimaryKey> :IDomainService where TEntity : class, IEntity<TPrimaryKey>
{
    public DomainService(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        this.EntityRepo = serviceProvider.GetRequiredService<IRepository<TEntity, TPrimaryKey>>();
    }
    public virtual IServiceProvider ServiceProvider { get; }
    public virtual IRepository<TEntity, TPrimaryKey> EntityRepo { get; }
}