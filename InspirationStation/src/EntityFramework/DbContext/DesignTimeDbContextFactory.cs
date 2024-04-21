using Core.Configuration;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFramework.DbContext;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InspirationStationDbContext>
{
    public InspirationStationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InspirationStationDbContext>();
        var basePath = WebContentDirectoryFinder.CalculateContentRootFolder();
        var configuration = AppConfigurations.Get(basePath, "Development");
        var connectionString = "Server=data.dev.52abp.com; Database=InspirationStation;User Id=root;Password=bb123456??;";
        // var connectionString = configuration.ConnectionStringsDefault();
        Console.WriteLine("迁移使用数据库连接字符串：");
        Console.WriteLine(connectionString);
        optionsBuilder.UseNpgsql(connectionString);
        return new InspirationStationDbContext(optionsBuilder.Options);
    }
}
