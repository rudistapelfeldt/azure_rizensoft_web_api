using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using System.IO;

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
                        else if (context.HostingEnvironment.IsStaging() || context.HostingEnvironment.IsProduction())
                        {
                            var builtConfig = configBuilder.Build();
                                configBuilder.AddAzureKeyVault(new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"), new DefaultAzureCredential());
                        }
                    });

                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
