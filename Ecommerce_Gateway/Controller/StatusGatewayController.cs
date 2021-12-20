using Ecommerce_Gateway.Model;
using Ecommerce_Gateway.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ecommerce_Gateway.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusGatewayController : ControllerBase
    {
        private readonly IConfigurationServices _config;
        private readonly HttpClient _http;



        public StatusGatewayController(
          IHttpClientFactory clientFactory,
          IConfigurationServices configurationService,
          IHttpContextAccessor httpContextAccessor,
          IHttpService httpServiceExtensions
        )
        {
            _config = configurationService;
            // Note: Below code initializes the HttpClient, ConfigurationService and setup the custom Authorization header for HttpClient
            if (httpServiceExtensions != null)
            {
                _http = httpServiceExtensions.InitializeConfiguration(clientFactory);
                httpServiceExtensions.SetAuthorizationHeaderForHttpClient(httpContextAccessor);
            }
        }


        //user and admin both
        // return a list of status from status service 
        [HttpGet]
        [Route("Get-All-Status-Gateway")]
        public async Task<IActionResult> getAllStatusGATEWAY()
        {
            using (_http)
            {
                _http.BaseAddress = new Uri(_config.getStatusesCatelogue);  // its the url
                _http.DefaultRequestHeaders.Accept.Clear(); // must need header due to caching filled with values so clear those values
                _http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));    // data type must be json what to send



                var result = await _http.GetAsync("get-all-status");
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }

            }

        }


        //admin
        [HttpPost("Add-Status-Gateway")]
        public async Task<Response> AddStatus(StatusCatalogue st)
        {

            using (_http) //client's object which is making a request which is furture responsed
            {
                _http.BaseAddress = new Uri(_config.getStatusesCatelogue);   // path to send the post data
                _http.DefaultRequestHeaders.Accept.Clear(); //not necessary in registers case it's a safe check
                _http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));    // data type must be json what to send

                var result = await _http.PostAsync("add-status", st.AsJson());   //await used as its async (hit on register method of usermanagement service which gives a response)
                //the above 2 cases AsJson converting c# obj to json                    
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();    //get data from user managment api on successfully registration of the user
                    return new Response { status = "Sucess", message = "Status registered sucessfully" };
                }
                else
                {
                    return new Response { status = "Error", message = "Status not registered sucessfully" };
                }
            }
        }
 



         
        


    }
}
