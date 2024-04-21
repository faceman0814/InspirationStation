namespace Core;

public abstract class AppConsts
{
    /// <summary>
    /// 默认语言
    /// </summary>
    public const string DefaultLanguage = "zh-Hans";
    
    /// <summary>
    /// 语言文件的名称
    /// </summary>
    public const string LocalizationSourceName = "InspirationStation";
    
    /// <summary>
    /// 表的前缀名称
    /// </summary>
    public const string TablePrefix = "FaceMan_";
    public static bool SwaggerUiEnabled = true;
    
    /// <summary>
    /// Token过期时间 默认1天
    /// </summary>
    public static TimeSpan AccessTokenExpiration = TimeSpan.FromDays(1);

    public static class System
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionStrings_Default { get; }
        static System()
        {
            ConnectionStrings_Default = "Default";
            // MultiTenancy_IsEnabled = "MultiTenancy:IsEnabled";
            // Authentication_JwtBearer_IsEnabled = "Authentication:JwtBearer:IsEnabled";
            // Authentication_JwtBearer_SecurityKey = "Authentication:JwtBearer:SecurityKey";
            // Authentication_JwtBearer_Issuer = "Authentication:JwtBearer:Issuer";
            // Authentication_JwtBearer_Audience = "Authentication:JwtBearer:Audience";
        }
    }
}