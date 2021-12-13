using Common;
using Common.Utility;
using Ecommerce_UserManagment.Identity;
using Ecommerce_UserManagment.Identity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_UserManagment
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
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
            services.AddHttpContextAccessor();
            services.AddControllers();

            //comment this
            //adding email confirmation
            //services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            //{
            //    opt.SignIn.RequireConfirmedEmail = true;

            //})
            //    AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();


            /*
                custom code below
                for Entity framework
                for identity framework
                for authentication
                for bearer token
                for email confirmation
             */

            // For Entity Framework
            // highlighted area

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //adding authentication
            //  services.AddAuthentication(options =>
            //  {
            //      var scheme = JwtBearerDefaults.AuthenticationScheme;
            //      options.DefaultAuthenticateScheme = scheme;
            //      options.DefaultChallengeScheme = scheme;
            //      options.DefaultScheme = scheme;
            //  })

            // // Adding Jwt Bearer 
            //. AddJwtBearer(options =>
            //   {
            //       options.SaveToken = true;
            //       options.RequireHttpsMetadata = false;
            //       options.TokenValidationParameters = new TokenValidationParameters()
            //       {
            //           ValidateIssuer = true,
            //           ValidateAudience = true,
            //           ValidAudience = Configuration["JWT:ValidAudience"],
            //           ValidIssuer = Configuration["JWT:ValidIssuer"],
            //           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JWT_SECRET"]))
            //       };
            //   });



            // Set the comments path for the Swagger JSON and UI.
            var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            services.AddSwaggerDocumentation("User Management API", "v1", xmlPath);

            //// adding email verification
            services.Configure<IdentityOptions>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 8;

                opts.SignIn.RequireConfirmedEmail = true;
            });


            /*
                custom code above
                for Entity framework
                for identity framework
                for authentication
                for bearer token
                for email confirmation
             */


            //JWT from class library reference added
            services.ConfigureJwtAuthentication(Configuration.GetValue<string>("AppSettings:JWT_SECRET"));

            services.AddMvc();
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerDocumentation("/swagger/v1/swagger.json", "User Management Api v1");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
