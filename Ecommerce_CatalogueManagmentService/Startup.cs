using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ecommerce_CatalogueManagmentService.Entities.DO;
using Ecommerce_CatalogueManagmentService.Business.BAL.Interface;
using Ecommerce_CatalogueManagmentService.Business.BAL;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;
using Ecommerce_CatalogueManagmentService.Repository.DAL;
using Common.Utility;

namespace Ecommerce_CatalogueManagmentService
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

            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddTransient<IStatusManager, StatusManager>();
            services.AddTransient<IStatusRepository, StatusRepository>();

            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();


            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<IProductRepository, ProductRepository>();


            services.AddTransient<ICategoryStatusManager, CategoryStatusManager>();
            services.AddTransient<IStatusCategoryRepository, StatusCategoryRepository>();






            //JWT from class library reference added
            services.ConfigureJwtAuthentication(Configuration.GetValue<string>("JWT:Secret"));



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce_CatalogueManagmentService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce_CatalogueManagmentService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
