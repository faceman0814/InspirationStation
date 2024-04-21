using FaceManUtils.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FaceMan.Utils.Swagger;

public class SwaggerOperationFilter : IOperationFilter
{
    /// <summary>
    /// 将OpenApiOperation对象中的参数属性Schema设置为对应枚举类型的模式。如果参数不是枚举类型，则跳过处理。
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
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