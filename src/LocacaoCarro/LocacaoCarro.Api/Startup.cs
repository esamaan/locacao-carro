using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using LocacaoCarro.Api.Filtros;
using LocacaoCarro.Api.Logging;
using LocacaoCarro.Api.Assemblies;
using LocacaoCarro.Api.IoC;

namespace LocacaoCarro.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(IWebHostEnvironment webHostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(webHostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{webHostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));

            services.AddLoggingSerilog();

            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());

            services.AddDependencyResolver();

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LocacaoCarro",
                    Description = "API - Locação de Carros",
                    Version = "v1"
                });

                //var apiPath = Path.Combine(AppContext.BaseDirectory, "LocacaoCarro.Api.xml");
                //var applicationPath = Path.Combine(AppContext.BaseDirectory, "LocacaoCarro.Aplicacao.xml");

                //c.IncludeXmlComments(apiPath);
                //c.IncludeXmlComments(applicationPath);
            });

            services.AddScoped<IDbConnection>(x => new SqlConnection(Environment.GetEnvironmentVariable("DB_LOCACAO_CARRO")));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/LocacaoCarro.Api");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/LocacaoCarro.Api/swagger/v1/swagger.json", "API LocacaoCarro");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
