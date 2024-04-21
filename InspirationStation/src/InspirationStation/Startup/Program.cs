using InspirationStation.Startup;
using Microsoft.AspNetCore;

public class Program
{
    public static int Main(string[] args)
    {
        var host = BuildWebHost(args);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        host.Run();
        return 0;
    }

    private static IWebHost BuildWebHost(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseIIS()
            .UseIISIntegration()
            .UseStartup<Startup>()
            // .UseSerilog()
            .Build();
    }
}