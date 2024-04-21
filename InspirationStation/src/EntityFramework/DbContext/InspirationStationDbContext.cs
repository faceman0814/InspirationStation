using Core.UserModule;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFramework.DbContext;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class InspirationStationDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // 可选，设置数据库编码，SQLServer
        //modelBuilder.UseCollation("Chinese_PRC_CI_AS");
        // 配置实体
        // modelBuilder.ConfigureInspirationStationApi();
        modelBuilder.ApplyConfigurationsFromAssembly((this.GetType().Assembly));
    }
    
    public InspirationStationDbContext(DbContextOptions<InspirationStationDbContext> options) : base(options)
    {
    }
}

