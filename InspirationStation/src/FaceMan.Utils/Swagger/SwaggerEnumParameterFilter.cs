using System.Collections;
using FaceManUtils.Extensions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FaceMan.Utils.Swagger;

 public class SwaggerEnumParameterFilter : IParameterFilter
  {
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
      Type type1 = Nullable.GetUnderlyingType(context.ApiParameterDescription.Type);
      if ((object) type1 == null)
        type1 = context.ApiParameterDescription.Type;
      Type type2 = type1;
      if (type2.IsEnum)
      {
        SwaggerEnumParameterFilter.AddEnumParamSpec(parameter, type2, context);
        parameter.Required = type2 == context.ApiParameterDescription.Type;
      }
      else
      {
        if (!type2.IsArray && (!type2.IsGenericType || !((IEnumerable<Type>) type2.GetInterfaces()).Contains<Type>(typeof (IEnumerable))))
          return;
        Type type3 = type2.GetElementType();
        if ((object) type3 == null)
          type3 = ((IEnumerable<Type>) type2.GenericTypeArguments).First<Type>();
        Type type4 = type3;
        SwaggerEnumParameterFilter.AddEnumSpec(parameter, type4, context);
      }
    }

    private static void AddEnumSpec(
      OpenApiParameter parameter,
      Type type,
      ParameterFilterContext context)
    {
      OpenApiSchema orAdd = context.SchemaRepository.Schemas.GetOrAdd<string, OpenApiSchema>("#/definitions/" + type.Name, (Func<OpenApiSchema>) (() => context.SchemaGenerator.GenerateSchema(type, context.SchemaRepository)));
      if (orAdd.Reference == null || !type.IsEnum)
        return;
      parameter.Schema = orAdd;
      OpenApiArray openApiArray = new OpenApiArray();
      openApiArray.AddRange((IEnumerable<IOpenApiAny>) ((IEnumerable<string>) Enum.GetNames(type)).Select<string, OpenApiString>((Func<string, OpenApiString>) (_ => new OpenApiString(_))));
      orAdd.Extensions.Add("x-enumNames", (IOpenApiExtension) openApiArray);
    }

    private static void AddEnumParamSpec(
      OpenApiParameter parameter,
      Type type,
      ParameterFilterContext context)
    {
      OpenApiSchema schema = context.SchemaGenerator.GenerateSchema(type, context.SchemaRepository);
      if (schema.Reference == null)
        return;
      parameter.Schema = schema;
      OpenApiArray openApiArray = new OpenApiArray();
      openApiArray.AddRange((IEnumerable<IOpenApiAny>) ((IEnumerable<string>) Enum.GetNames(type)).Select<string, OpenApiString>((Func<string, OpenApiString>) (_ => new OpenApiString(_))));
      schema.Extensions.Add("x-enumNames", (IOpenApiExtension) openApiArray);
    }
  }