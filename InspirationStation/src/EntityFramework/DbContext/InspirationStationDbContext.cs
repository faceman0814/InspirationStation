using Core.UserModule;

namespace EntityFramework.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
public class InspirationStationDbContext(IConfiguration appConfiguration) : DbContext
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
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=data.dev.52abp.com; Database=InspirationStation;User Id=root;Password=bb123456??;");
        // if (!optionsBuilder.IsConfigured)
        // {
        //     optionsBuilder.UseNpgsql(appConfiguration.GetConnectionString("Host=data.dev.52abp.com; Database=InspirationStation;User Id=root;Password=bb123456??;"));
        // }
    }
}