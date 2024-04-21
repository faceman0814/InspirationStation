using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FaceMan.Utils.Swagger;


public abstract class SwaggerOperationIdFilter : IOperationFilter
{
    /// <summary>
    /// 为给定的OpenApiOperation对象设置OperationId属性。
    /// </summary>
    /// <param name="operation">要操作的OpenApiOperation对象。</param>
    /// <param name="context">操作过滤器的上下文，包含有关正在进行的API操作的信息。</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.OperationId = FriendlyId(context.ApiDescription);
    }

    /// <summary>
    /// 生成一个友好的API标识符。代码接受一个ApiDescription对象作为参数。
    /// </summary>
    /// <param name="apiDescription">API的描述信息，包括请求路径、HTTP方法等。</param>
    /// <returns></returns>
    private static string FriendlyId(ApiDescription apiDescription)
    {
        var parts = (RelativePathSansQueryString(apiDescription) + "/" + apiDescription.HttpMethod.ToLower())
            .Split('/');

        var builder = new StringBuilder();
        foreach (var part in parts)
        {
            var trimmed = part.Trim('{', '}');
            builder.AppendFormat("{0}{1}",
                (part.StartsWith("{") ? "By" : string.Empty),
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(trimmed)
            );
        }

        return builder.ToString();
    }

    /// <summary>
    /// 从给定的ApiDescription对象中提取相对路径（不包含查询字符串），并返回该相对路径。
    /// </summary>
    /// <param name="apiDescription">要提取相对路径的ApiDescription对象。</param>
    /// <returns></returns>
    private static string RelativePathSansQueryString(ApiDescription apiDescription)
    {
        return apiDescription.RelativePath?.Split('?').First();
    }
}