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
        modelBuilder.Entity<OrganizationUnit>(entity =>
        {
            // 定义组织单元与父组织单元之间的一对多关系
            entity.HasOne(e => e.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(e => e.ParentOrgUnitID) // 指定外键
                .IsRequired(false); // 表明ParentOrgUnitID可以为空，表示顶级单位
        
            // 如果需要，还可以添加其他配置，例如索引、默认值等
        });
        modelBuilder.ApplyConfigurationsFromAssembly((this.GetType().Assembly));
    }
}

