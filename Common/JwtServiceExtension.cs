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
        public static void ConfigureJwtAuthentication(this IServiceCollection services, string JWTSecertKey)
        {
            var _jwtsecertkey = Encoding.ASCII.GetBytes(JWTSecertKey);
            services.AddAuthentication(
              x =>
              {
                  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

              }).AddJwtBearer(
              x =>
              {
                  x.RequireHttpsMetadata = false;
                  x.SaveToken = true;
                  x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(_jwtsecertkey),
                      ValidateIssuer = false,
                      ValidateAudience = false
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
                          var path = context.HttpContext.Request.Path;
                          if (!string.IsNullOrEmpty(accessToken))
                          {
                              var token = JsonConvert.DeserializeObject<string>(accessToken.ToString()).Replace("Bearer ","");
                              context.Token = token;
                          }
                          return Task.CompletedTask;
                      }
                  };
              });
        }
    }
}
