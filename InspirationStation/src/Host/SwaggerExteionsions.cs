using FaceMan.Utils.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host;


public static class SwaggerExteionsions
{
    /// <summary>配置使用枚举处理器</summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static SwaggerGenOptions UseEnumSchemaFilter(this SwaggerGenOptions options)
    {
        options.SchemaFilter<SwaggerEnumSchemaFilter>();
        return options;
    }

    /// <summary>
    /// 配置使用 <see cref="T:Swagger.SwaggerEnumParameterFilter" />
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static SwaggerGenOptions UseEnumParameterFilter(this SwaggerGenOptions options)
    {
        options.ParameterFilter<SwaggerEnumParameterFilter>();
        return options;
    }

    /// <summary>
    /// 配置使用 <see cref="T:Swagger.SwaggerOperationIdFilter" />
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static SwaggerGenOptions UseOperationIdFilter(this SwaggerGenOptions options)
    {
        options.OperationFilter<SwaggerOperationIdFilter>();
        return options;
    }

    /// <summary>
    /// 配置使用 <see cref="T:Swagger.SwaggerOperationFilter" />
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static SwaggerGenOptions UseOperationFilter(this SwaggerGenOptions options)
    {
        options.OperationFilter<SwaggerOperationFilter>();
        return options;
    }

    /// <summary>触发生成swagger文档</summary>
    /// <param name="app"></param>
    /// <param name="action">初始化调用方法</param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerProvider(
        this IApplicationBuilder app,
        Action<ISwaggerProvider> action)
    {
        if (app.ApplicationServices.GetService(typeof (ISwaggerProvider)) is ISwaggerProvider service && action != null)
            action(service);
        return app;
    }
}