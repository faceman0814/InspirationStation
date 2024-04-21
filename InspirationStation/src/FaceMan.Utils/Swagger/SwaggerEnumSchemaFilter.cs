using System.ComponentModel;
using System.Reflection;
using FaceMan.Utils.Helper.EnumHelper;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FaceMan.Utils.Swagger;

/// <summary>
/// 枚举处理器，将会把所有的枚举信息放到 <see cref="P:EnumHelper.EnumConsts.BaseEnumMaps" /> 中
/// </summary>
public class SwaggerEnumSchemaFilter : ISchemaFilter
{
    private static readonly Type _displayNameAttributeType = typeof (DescriptionAttribute);

    /// <summary>
    /// 用于生成Swagger文档的Schema过滤器方法
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        Type type = context.Type;
        if (!type.IsEnum || schema.Extensions.ContainsKey("x-enumNames"))
            return;
        OpenApiArray openApiArray = new OpenApiArray();
        openApiArray.AddRange((IEnumerable<IOpenApiAny>) ((IEnumerable<string>) Enum.GetNames(type)).Select<string, OpenApiString>((Func<string, OpenApiString>) (_ => new OpenApiString(_))));
        schema.Extensions.Add("x-enumNames", (IOpenApiExtension) openApiArray);
        if (EnumConsts.BaseEnumMaps.ContainsKey(type.Name))
            return;
        EnumConsts.BaseEnumMaps.Add(type.Name, new List<EnumMap>());
        string[] enumNames = type.GetEnumNames();
        List<FieldInfo> list = ((IEnumerable<FieldInfo>) type.GetFields()).ToList<FieldInfo>();
        if (enumNames == null || enumNames.Length == 0)
            return;
        foreach (string str in enumNames)
        {
            string name = str;
            FieldInfo element = list.Find((Predicate<FieldInfo>) (o => o.Name == name));
            Attribute customAttribute = element.GetCustomAttribute(SwaggerEnumSchemaFilter._displayNameAttributeType);
            if (customAttribute != null && customAttribute is DescriptionAttribute descriptionAttribute)
                EnumConsts.BaseEnumMaps[type.Name].Add(new EnumMap()
                {
                    Key = Convert.ToInt32(element.GetValue((object) type)),
                    Value = element.Name,
                    DisplayName = descriptionAttribute.Description
                });
        }
    }
}