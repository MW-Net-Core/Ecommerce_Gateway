using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Common.Utility
{
    public static class JwtServiceExtension
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, string jwtSecretKeyString)
        {
            var JwtSecretKey = Encoding.ASCII.GetBytes(jwtSecretKeyString);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(JwtSecretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // NOTE: the default for this setting is 5 min

                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    },

                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // NOTE: If the request is for our hub...
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hubs")))
                        {
                            // Read the token out of the query string
                            var token = JsonConvert.DeserializeObject<string>(accessToken.ToString()).Replace("Bearer ", "");
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    },

                    //OnTokenValidated = (ctx) =>
                    //{
                    //    return Task.CompletedTask;
                    //}
                };
            });
        }
    }
}
