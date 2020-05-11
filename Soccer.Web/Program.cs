using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Soccer.Web
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //public static void Main(string[] args)
        //{
        //    IWebHost host = CreateWebHostBuilder(args).Build();
        //    RunSeeding(host);
        //    host.Run();
        //}

        //private static void RunSeeding(IWebHost host)
        //{
        //    IServiceScopeFactory scopeFactory = host.Services.GetService<IServiceScopeFactory>();
        //    using (IServiceScope scope = scopeFactory.CreateScope())
        //    {
        //        SeedDb seeder = scope.ServiceProvider.GetService<SeedDb>();
        //        seeder.SeedAsync().Wait();
        //    }
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        //{
        //    return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
        //}
    }
}
