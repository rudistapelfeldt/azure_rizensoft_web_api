using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using webapi.Models;

namespace Webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, configBuilder) =>
                    {
                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.Development.json", true)
                                .AddEnvironmentVariables();
                        }
                    });

                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://localhost:7290", "https://rizensoft-azure-api-apim.azure-api.net");
                });
        }
    }
}
