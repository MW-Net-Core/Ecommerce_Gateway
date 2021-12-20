using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;

namespace Common
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string apiTitle, string apiVersion, string apiXmlPath)
        {
            services.AddSwaggerGen(c =>
            {
                // c.CustomSchemaIds(y => y.FullName);
                c.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiTitle, Version = apiVersion });

                c.IncludeXmlComments(apiXmlPath);

                //First we define the security scheme
                c.AddSecurityDefinition("Bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme // Key
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer", // Note: The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>() // Value
                    }
                });
            });

            return services;
        }


        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, string swaggerJsonName, string apiTitle)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);

                c.SwaggerEndpoint(swaggerJsonName, apiTitle);
                //c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
