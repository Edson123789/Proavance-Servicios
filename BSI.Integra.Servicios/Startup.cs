using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppOptics.Instrumentation;
using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.DTOs.Scode.AutoMap;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Models.AulaVirtual;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackifyLib;
using Swashbuckle.AspNetCore.Swagger;
using BSI.Integra.Servicios.GlobalErrorHandling.CustomExceptionMiddleware;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Http.Features;

namespace BSI.Integra.Servicios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Add DB
            services.AddDbContext<integraDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IntegraDB")));
            services.AddDbContext<AulaVirtualContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("AulaVirtualDB")));

            //Instance injection
            services.AddScoped(typeof(IAutoMapConverter<,>), typeof(AutoMapConverter<,>));
            services.AddScoped<IUser, UserImpl>();
            services.AddScoped(typeof(IIntegraRepository<>), typeof(IntegraRepository<>));
            services.AddScoped(typeof(IIntegraVistaRepository<>), typeof(IntegraVistaRepository<>));
            services.AddScoped(typeof(IDapperRepository), typeof(DapperRepository));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "Integra V4.0 API", Version = "v1" });
                c.OperationFilter<FormFileSwaggerFilter>();
            });

            //Add mappers
            MapperConfig.RegistarMapp();

            //Add framework services
            services.AddMvc(options =>
            {
                //options.Filters.Add(new ApiExceptionFilter());                
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(opt =>
            {
                var resolver = opt.SerializerSettings.ContractResolver;
                if (resolver != null)
                {
                    var res = resolver as DefaultContractResolver;
                    res.NamingStrategy = null;
                }
            });

            services.AddCors();
            services.Configure<FormOptions>(x =>
            {
                x.ValueCountLimit = 8192;
                x.MultipartBodyLengthLimit = 524288000; //500MB
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAllElasticApm(Configuration);
            app.UseAppopticsApm();
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpMethodOverride();
            app.UseHttpsRedirection();
            loggerFactory.AddSerilog();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Integra V4.0 API"); });

            //add log for retrace
            app.ConfigureStackifyLogging(Configuration); //This is critical!!
            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            app.ConfigureCustomExceptionMiddleware();

            app.UseMvc();
        }
    }
}
