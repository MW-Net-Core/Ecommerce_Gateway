using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ecommerce_Gateway.Utilities
{
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;

        
        public HttpClient InitializeConfiguration(IHttpClientFactory clientfactory)
        {
            _httpClient = clientfactory.CreateClient();
            return _httpClient;
        }

        public void SetAuthorizationHeadersForHttpClients(IHttpContextAccessor httpContextAccessor)
        {
            throw new NotImplementedException();
        }
    }
}
