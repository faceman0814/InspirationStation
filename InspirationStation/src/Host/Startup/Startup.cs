using Core.Configuration;
using EntityFramework.DbContext;
using Microsoft.EntityFrameworkCore;

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
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}