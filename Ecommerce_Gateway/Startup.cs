using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.Features;
using Ecommerce_Gateway.Utilities;
using Common.Utility;
using Common;
using System;
using System.Reflection;
using System.IO;

namespace Ecommerce_Gateway
{
    public class Startup
    {
        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            // Note: configure appsettings.json according to specific environment
            var contentRootPath = env.ContentRootPath;
            var builder = new ConfigurationBuilder()
                    .SetBasePath(contentRootPath);

            if (env.IsDevelopment())
            {
                builder
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                Configuration = builder.Build();
            }

            if (env.IsProduction())
            {
                builder
                    .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                Configuration = builder.Build();
            }

            if (env.IsStaging())
            {
                builder
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                Configuration = builder.Build();
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region //Allow origin
            var baseUrl = Configuration.GetValue<string>("AppSettings:WebBaseUri");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(baseUrl)
                                            .AllowAnyHeader()
                                            .AllowAnyOrigin()
                                            .AllowAnyMethod();
                    });

                options.AddPolicy("AnotherPolicy",
                builder =>
                {
                    builder.WithOrigins(baseUrl)
                                        .AllowAnyHeader()
                                        .AllowAnyOrigin()
                                        .AllowAnyMethod();
                });
            });
            #endregion



            services.AddHttpContextAccessor();
            services.AddControllers();

            // Set the comments path for the Swagger JSON and UI.
            var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            services.AddSwaggerDocumentation("Ecommerce_Gateway", "v1", xmlPath);

            services.AddHttpContextAccessor();
            services.AddTransient<IConfigurationServices, ConfigurationServices>();
            services.AddTransient<IHttpService, HttpService>();
            services.AddHttpClient();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                //options.Filters.Add(new AuditLoggingFilter());
            })
               .AddNewtonsoftJson()
               .AddJsonOptions(opt =>
               {
                   opt.JsonSerializerOptions.PropertyNamingPolicy = null;
               });

            services.AddControllers().AddNewtonsoftJson();

            // Note: Added to allow image file uploading
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            //JWT from class library reference added
            services.ConfigureJwtAuthentication(Configuration.GetValue<string>("AppSettings:Secret"));

            services.AddSession();
            services.AddSignalR();
            // Note: Added to allow image file uploading
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerDocumentation("/swagger/v1/swagger.json", "Ecommerce_Gateway v1");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce_Gateway v1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               // endpoints.MapHub<NotificationHub>("/hubs/notification");
                endpoints.MapControllers();
            });
        }
    }
}
