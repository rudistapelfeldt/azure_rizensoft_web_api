using System.Web.Http;
using Swashbuckle.Application;

namespace webapi.Swagger
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config
            .EnableSwagger(c =>
            {
                c.ApiKey("apiKey")
                    .Description("API Key for accessing secure APIs")
                    .Name("Api-Key")
                    .In("header");
            })
            .EnableSwaggerUi(c =>
            {
                c.EnableApiKeySupport("Api-Key", "header");
            });
        }
    }
}

