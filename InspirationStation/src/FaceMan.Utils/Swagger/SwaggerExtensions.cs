using System.Text;
using FaceManUtils.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace FaceMan.Utils.Swagger
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// 在SwaggerUIOptions对象中注入一个基础URL路径。它将给定的pathBase参数添加到options.HeadContent属性中的JavaScript代码中，以便在Swagger UI加载时设置abp.appPath变量。
        /// </summary>
        /// <param name="options"> </param>
        /// <param name="pathBase"> base path (URL) to application API </param>
        public static void InjectBaseUrl(this SwaggerUIOptions options, string pathBase)
        {
            pathBase = pathBase.EnsureEndsWith('/');

            options.HeadContent = new StringBuilder(options.HeadContent)
                .AppendLine($"<script> var abp = abp || {{}}; abp.appPath = abp.appPath || '{pathBase}'; </script>")
                .ToString();
        }

        /// <summary>
        /// https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/752#issuecomment-467817189
        /// 当 Swashbuckle.AspNetCore 5.0 发布时，我们可以将其删除。
        /// 自定义Swagger的默认模式ID选择器。
        /// </summary>
        /// <param name="options"> </param>
        public static void CustomDefaultSchemaIdSelector(this SwaggerGenOptions options)
        {
            string SchemaIdSelector(Type modelType)
            {
                if (!modelType.IsConstructedGenericType)
                {
                    return modelType.Name;
                }

                var prefix = modelType.GetGenericArguments()
                    .Select(SchemaIdSelector)
                    .Aggregate<string>((previous, current) => previous + current);

                return modelType.Name.Split('`').First() + "Of" + prefix;
            }

            options.CustomSchemaIds(SchemaIdSelector);
        }
    }
}
