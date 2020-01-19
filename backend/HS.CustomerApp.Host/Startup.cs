using FluentValidation;
using FluentValidation.AspNetCore;
using HS.CustomerApp.Host.Logic;
using HS.CustomerApp.Host.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HS.CustomerApp.Host
{
    public class Startup
    {
        readonly string CorsPolicyName = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation();
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddTransient<IValidator<CustomerModel>, CustomerValidator>();

            services.AddOpenApiDocument(c => c.Title = "CustomerApp REST-API");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsPolicyName);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc();
        }
    }
}