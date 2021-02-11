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
using System.Collections.Generic;
using Microsoft.Extensions.PlatformAbstractions;
using System.Linq;

namespace LocacaoCarro.Api
{
    /// <summary>
    /// Application startup
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="webHostEnvironment"></param>
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

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));

            services.AddLoggingSerilog();

            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());

            services.AddDependencyResolver();

            services.AddHealthChecks();

            ConfigureSwagger(services);

            services.AddScoped<IDbConnection>(x => new SqlConnection(Configuration.GetConnectionString("SqlServer")));

        }

        /// <summary>
        /// Configura o swagger
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureSwagger(IServiceCollection services)
        {
            var infoV1 = new OpenApiInfo
            {
                Title = "LocacaoCarro - V1",
                Version = "v1",
                Description = "API - Locação de Carros"
            };

            var securityScheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Adicione a palavra 'bearer' e o seu token JWT",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            };

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>{ }
                }
            };

            string path = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{PlatformServices.Default.Application.ApplicationName}.xml");

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", infoV1);
                x.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                x.IncludeXmlComments(path);
                x.AddSecurityDefinition("Bearer", securityScheme);
                x.AddSecurityRequirement(securityRequirement);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API LocacaoCarro");
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
