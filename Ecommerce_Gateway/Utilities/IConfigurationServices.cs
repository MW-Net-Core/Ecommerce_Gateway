using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Gateway.Utilities
{
    public interface IConfigurationServices
    {
        IConfiguration Configuration { get; }
        IHttpContextAccessor HttpContextAccessor { get; } 
        Uri UserMangementBaseUri { get; }
        Guid UserLoggedInId { get; }
        string Authenticate { get; }
    }
}
