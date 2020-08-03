using Data;
using Data.Repositories;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Services;
using AutoMapper;
using API.Middlewares;
using Data.Configurations;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers();

            SetUpConfigurations(services);
            Register(services);
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdventureWorks.OrderApi", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Swagger API Documentation
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdventureWorks.OrderApi v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            // Created custom Middleware to handle all exceptions and produce error response user
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                // To report the health of app
                endpoints.MapHealthChecks("/api/health");
                endpoints.MapControllers();
            });
        }

        public void SetUpConfigurations(IServiceCollection services)
        {
            //Binding the all db Configurations to connectionStrings
            services.Configure<DbConfiguration>(options =>
                Configuration.GetSection("DbConfiguration").Bind(options));
        }

        public void Register(IServiceCollection services)
        {
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            services.AddScoped<ISalesOrderHeaderService, SalesOrderHeaderService>();
            services.AddScoped<ISalesOrderHeaderRepository, SalesOrderHeaderRepository>();
        }
    }
}
