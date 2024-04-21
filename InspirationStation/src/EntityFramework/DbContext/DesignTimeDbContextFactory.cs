using Core.Configuration;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFramework.DbContext;

/// <summary>
/// 创建 DbContext 工厂类，用于迁移数据库
/// 在EF的设计时环境中，通常无法直接从依赖注入容器中获取数据库上下文或使用连接字符串来创建上下文实例。这是通过实现IDesignTimeDbContextFactory接口，手动创建数据库上下文的实例。
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InspirationStationDbContext>
{
    public InspirationStationDbContext CreateDbContext(string[] args)
    {
        // 迁移使用数据库连接字符串
        var optionsBuilder = new DbContextOptionsBuilder<InspirationStationDbContext>();
        // 获取基础路径
        var basePath = WebContentDirectoryFinder.CalculateContentRootFolder();
        // 获取配置
        var configuration = AppConfigurations.Get(basePath, "Development");
        // 打印数据库连接字符串
        var connectionString = configuration.ConnectionStringsDefault();
        Console.WriteLine("迁移使用数据库连接字符串：{0}",connectionString);
        // 迁移数据库连接字符串
        optionsBuilder.UseNpgsql(connectionString);
        // 创建数据库上下文
        return new InspirationStationDbContext(optionsBuilder.Options);
    }
}