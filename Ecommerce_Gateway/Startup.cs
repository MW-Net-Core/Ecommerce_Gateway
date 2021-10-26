using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.Features;
using Ecommerce_Gateway.Utilities;

namespace Ecommerce_Gateway
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
            #region //Allow origin
            //var baseUrl = Configuration.GetValue<string>("AppSettings:WebBaseUri");

            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder =>
            //        {
            //            builder.WithOrigins(baseUrl)
            //                                .AllowAnyHeader()
            //                                .AllowAnyOrigin()
            //                                .AllowAnyMethod();
            //        });

            //    options.AddPolicy("AnotherPolicy",
            //    builder =>
            //    {
            //        builder.WithOrigins(baseUrl)
            //                            .AllowAnyHeader()
            //                            .AllowAnyOrigin()
            //                            .AllowAnyMethod();
            //    });
            //});
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce_Gateway", Version = "v1" });
            });
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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce_Gateway v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
