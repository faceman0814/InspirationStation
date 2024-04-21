using FaceManUtils.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FaceMan.Utils.Swagger;

public class SwaggerOperationFilter : IOperationFilter
{
    /// <summary>
    /// 将OpenApiOperation对象中的参数中的枚举类型替换为对应的schema。
    /// </summary>
    /// <param name="operation"> OpenApiOperation对象，表示一个API操作。</param>
    /// <param name="context">OperationFilterContext对象，包含了操作筛选器的上下文信息。</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            return;
        }

        for (var i = 0; i < operation.Parameters.Count; ++i)
        {
            var parameter = operation.Parameters[i];

            var enumType = context.ApiDescription.ParameterDescriptions[i].ParameterDescriptor.ParameterType;
            if (!enumType.IsEnum)
            {
                continue;
            }

            var schema = context.SchemaRepository.Schemas.GetOrAdd($"{enumType.Name}", () =>
                context.SchemaGenerator.GenerateSchema(enumType, context.SchemaRepository)
            );

            parameter.Schema = schema;
        }
    }
}