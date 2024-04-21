using Core;
using Core.Configuration;
using EntityFramework.DbContext;
using FaceMan.Utils.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Host.Startup;

public class Startup
{
    private readonly IConfigurationRoot _appConfiguration;
    private readonly IWebHostEnvironment _env;

    public Startup(IWebHostEnvironment env)
    {
        _env = env;
        _appConfiguration = _env.GetAppConfiguration();
        // InitWebConsts();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        
        services.AddDbContext<InspirationStationDbContext>(options =>
        {
            options.UseNpgsql(_appConfiguration.GetConnectionString("Default"));
        });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        services.AddControllers();
        // services.AddAuthentication();
        // services.AddAuthorization();
        // 配置SwaggerUI
        ConfigureSwaggerUiService(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (_env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            // app.UseBrowserLink();
        }
        else
        {
            app.UseStatusCodePagesWithRedirects("~/Error?statusCode={0}");
            app.UseExceptionHandler("/Error");
        }

        // 启用CORS
        app.UseCors(builder => builder.AllowAnyOrigin());
        // app.UseCors(_defaultCorsPolicyName);
        // 启用静态文件
        app.UseStaticFiles();
        // 启用路由
        app.UseRouting();
        // 启用校验
        app.UseAuthentication();
        // 启用HTTP请求
        app.UseHttpsRedirection();
        // 启用SwaggerUI
        app.UseSwaggerProvider(swaggerProvider =>
        {
            swaggerProvider.GetSwagger("v1", null, "");
        });

        //配置路由
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
        });

        #region 配置swagerui

        if (AppConsts.SwaggerUiEnabled)
        {
            // 使中间件能够作为JSON端点提供生成的Swagger
            app.UseSwagger();
            // 使中间件能够提供swagger-ui(HTML、JS、CSS等)
            app.UseSwaggerUI(options =>
            {
                // SwaggerEndPoint
                options.SwaggerEndpoint(
                    _appConfiguration["App:SwaggerEndPoint"],
                    "InspirationStation API V1"
                );
                options.EnableDeepLinking();
                options.DocExpansion(DocExpansion.None);
                
            }); 
        } 
        #endregion 配置swagerui
    }

    private void ConfigureSwaggerUiService(IServiceCollection services)
    {
        #region 配置SwaggerUI

        if (AppConsts.SwaggerUiEnabled)
        {
            //Swagger -启用此行以及Configure方法中的相关行，以启用Swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "InspirationStation API",
                        Version = "v1",
                        Description = "InspirationStation 的动态WEBAPI管理端，可以进行测试和调用API。",
                        TermsOfService = new Uri("https://github.com/faceman0814"),
                        Contact = new OpenApiContact
                        {
                            Name = "InspirationStation",
                            Email = "1002784867@qq.com",
                            Url = new Uri("https://blog.faceman.cn")
                        },
                    });
                
                // 使用 camel case 的枚举
                //options.DescribeStringEnumsInCamelCase();
                
                //使用相对路径获取应用程序所在目录
                options.DocInclusionPredicate((docName, description) => true);
                // 支持非body内容中的枚举
                options.ParameterFilter<SwaggerEnumParameterFilter>();
                // 对应client枚举转为字符串对应值
                options.SchemaFilter<SwaggerEnumSchemaFilter>();
                options.OperationFilter<SwaggerOperationIdFilter>();
                options.OperationFilter<SwaggerOperationFilter>();
                options.CustomDefaultSchemaIdSelector();
                options.OrderActionsBy(x => x.RelativePath);
                options.DescribeAllParametersInCamelCase();
                ConfigApiDoc(options);
            });

            // 使用 newtonsoft.json 做swagger的序列化工具
            services.AddSwaggerGenNewtonsoftSupport();
            
        }

        #endregion 配置SwaggerUI
    }

    /// <summary>
    /// 配置类库的注释,开发时使用
    /// </summary>
    /// <param name="options"> </param>
    private void ConfigApiDoc(SwaggerGenOptions options)
    {
#if DEBUG
        //遍历所有xml并加载
        var binXmlFiles =
            new DirectoryInfo(Path.GetDirectoryName(typeof(Program).Assembly.Location))
                .GetFiles("*.xml", SearchOption.TopDirectoryOnly);
        foreach (var filePath in binXmlFiles.Select(item => item.FullName))
        {
            options.IncludeXmlComments(filePath, true);
        }
#endif
    }
}