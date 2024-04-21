using Microsoft.Extensions.Configuration;

namespace Core.Configuration;

public static class AppSettingsExtensions
{
    /// <summary>
    /// 默认数据库连接字符串
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static string ConnectionStringsDefault(this IConfiguration configuration)
    {
        return configuration.GetConnectionString(AppConst.System.ConnectionStrings_Default);

    }
}