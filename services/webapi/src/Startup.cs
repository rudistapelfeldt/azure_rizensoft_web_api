using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Webapi.Domains;
using Webapi.Interfaces;
using Webapi.Models;
using Webapi.Services;

namespace Webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AddressDomain>()
                .AddScoped<UserDomain>()
                .AddScoped<AuthenticationDomain>()
                .AddScoped<TokenDomain>()
                .AddScoped<ITokenService, TokenService>();

            services.AddControllers();

            services.AddApplicationInsightsTelemetry();

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });

            string connectionString = this.Configuration.GetValue<string>("SQLCONNSTRING");
            services.AddDbContext<RizenSoftDBContext>(builder =>
            {
                builder.UseSqlServer(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Azure RizenSoft Web Api v1");
                c.RoutePrefix = string.Empty;
                c.ConfigObject.DefaultModelRendering = Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model;
                c.ConfigObject.ShowCommonExtensions = true;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
