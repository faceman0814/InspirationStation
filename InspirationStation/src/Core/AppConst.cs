using System;

namespace CaiBlogApi.Core;

public abstract class AppConst
{
    /// <summary>
    /// 默认语言
    /// </summary>
    public const string DefaultLanguage = "zh-Hans";
    
    /// <summary>
    /// 语言文件的名称
    /// </summary>
    public const string LocalizationSourceName = "CaiBlogApi";
    
    /// <summary>
    /// 表的前缀名称
    /// </summary>
    public const string TablePrefix = "FaceMan_";
    
    /// <summary>
    /// Token过期时间 默认1天
    /// </summary>
    public static TimeSpan AccessTokenExpiration = TimeSpan.FromDays(1);
}