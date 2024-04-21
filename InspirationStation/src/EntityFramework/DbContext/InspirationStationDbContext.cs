using Core.UserModule;

namespace EntityFramework.DbContext;

using Microsoft.EntityFrameworkCore;

public class InspirationStationDbContext(DbContextOptions<InspirationStationDbContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 可选，设置数据库编码，SQLServer
        //modelBuilder.UseCollation("Chinese_PRC_CI_AS");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly((this.GetType().Assembly));
    }
}

