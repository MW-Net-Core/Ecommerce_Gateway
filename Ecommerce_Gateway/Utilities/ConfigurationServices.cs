using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce_Gateway.Utilities
{
    public class ConfigurationServices : IConfigurationServices
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ConfigurationServices(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _config = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserLoggedInId
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                {
                    var userIdentity = HttpContextAccessor.HttpContext.User.Identity;
                    if (userIdentity.IsAuthenticated)
                    {
                        var claims = userIdentity as ClaimsIdentity;
                        return Guid.Parse(claims.FindFirst("UserId").Value);
                    } else {
                        return Guid.NewGuid();
                    }
                }
                else
                {
                    return Guid.NewGuid();
                }
            }
        }



        public IConfiguration Configuration => throw new NotImplementedException();

        public IHttpContextAccessor HttpContextAccessor => throw new NotImplementedException();

        public Uri UserMangementBaseUri
        {
            get
            {
                var uri = _config.GetValue<string>("AppSettings:UserManagmentBaseURI");
                return new Uri(uri);
            }
        }

    }
}
