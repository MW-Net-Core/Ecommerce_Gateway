using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Ecommerce_Gateway.Utilities
{
    public class HttpService : IHttpService
    {
        private HttpClient _client;


        public HttpClient InitializeConfiguration(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
            return _client;
        }


        public void SetAuthorizationHeaderForHttpClient(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authorizationHeader);

                httpContextAccessor.HttpContext.Request.Headers.TryGetValue("currentOrganizationId", out Microsoft.Extensions.Primitives.StringValues currentOrganizationHeader);

                if (!string.IsNullOrEmpty(authorizationHeader))
                {
                    var token = authorizationHeader.ToString().Replace("Bearer ", string.Empty, StringComparison.OrdinalIgnoreCase);

                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                if (!string.IsNullOrEmpty(currentOrganizationHeader))
                {
                    _client.DefaultRequestHeaders.Add("CurrentOrganizationId", currentOrganizationHeader.ToString());
                }
            }
        }
    }
}
