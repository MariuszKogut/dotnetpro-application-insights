using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HS.CustomerApp.HostConfiguration
{
    public static class SwaggerExtension
    {
        public static void AddCustomizedSwagger(this IServiceCollection services)
        {
            services.AddOpenApiDocument(c => c.Title = "CustomerApp REST-API");
        }

        public static void UseCustomizedSwagger(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc();
        }
    }
}