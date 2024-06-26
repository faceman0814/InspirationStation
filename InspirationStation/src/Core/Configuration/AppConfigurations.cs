﻿using System.Collections.Concurrent;
using FaceMan.Utils.Extensions;
using FaceManUtils.Extensions;
using Microsoft.Extensions.Configuration;

namespace Core.Configuration;

public static class AppConfigurations
{
    private static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

    static AppConfigurations()
    {
        ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
    }

    public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
    {
        string cacheKey = path + "#" + environmentName + "#" + addUserSecrets;

        return ConfigurationCache.GetOrAdd(
            cacheKey,
            _ => BuildConfiguration(path, environmentName, addUserSecrets)
        );
    }

    /// <summary>
    /// 根据环境变量来加载 appsettings.json的内容
    /// </summary>
    /// <param name="path"></param>
    /// <param name="environmentName"></param>
    /// <param name="addUserSecrets"></param>
    /// <returns></returns>
    private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null,
        bool addUserSecrets = false)
    {
        // 加载配置文件
        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", true, true);

        if (!String.IsNullOrWhiteSpace(environmentName))
        {
            builder = builder.AddJsonFile($"appsettings.{environmentName}.json", true);
        }

        builder = builder.AddEnvironmentVariables();

        if (addUserSecrets)
        {
            builder.AddUserSecrets(typeof(AppConfigurations).GetAssembly());
        }

        return builder.Build();
    }
}