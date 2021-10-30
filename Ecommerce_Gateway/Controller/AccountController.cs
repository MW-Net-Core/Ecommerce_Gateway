using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_Gateway.Utilities;
using System.Net.Http;
using Ecommerce_Gateway.Model;
using Newtonsoft.Json;
using System.Text;

namespace Ecommerce_Gateway.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfigurationServices _config;
        private readonly HttpClient _http;


        public AccountController(
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

        [HttpPost("Register")]
        public async Task<Response> Register(Register reg)
        {

            using (_http) //client's object which is making a request which is furture responsed
            {
                _http.BaseAddress = new Uri(_config.Authenticate);   // path to send the post data
                _http.DefaultRequestHeaders.Accept.Clear(); //not necessary in registers case it's a safe check
                _http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));    // data type must be json what to send

                if (reg.Password.Equals(reg.ConfirmPassword))   //checking password
                {
                    #region // Deprecated
                    //var payload = new Dictionary<string, string>
                    //{
                    //  {"UserName", reg.UserName},
                    //  {"Email", reg.Email},
                    //  {"Password", reg.Password}
                    //};

                    //string strPayload = JsonConvert.SerializeObject(payload);   // data conversion to json
                    //HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    #endregion


                    var result = await _http.PostAsync("Register", reg.AsJson());   //await used as its async (hit on register method of usermanagement service which gives a response)

                    //the above 2 cases AsJson converting c# obj to json

                    
                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();    //get data from user managment api on successfully registration of the user
                        return new Response { status = "Sucess", message = "Client registered sucessfully" };

                    }
                    else
                    {
                        return new Response { status = "Error", message = "Client not registered sucessfully" };
                    }

                }
                else  //password doesn't matched
                {
                    return new Response { status = "Error", message = "Password Does not match" };
                }
            }
        }


        [HttpPost("Login")]
        public async Task<Response> Login(Login Log)
        {
            using (_http)
            {
                _http.BaseAddress = new Uri(_config.Authenticate);  // authentication ka main controller hai
                _http.DefaultRequestHeaders.Accept.Clear(); // must need header due to caching filled with values so clear those values
                _http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));    // data type must be json what to send



                if (!(Log.UserName.Equals("") || Log.Password.Equals("")))
                {
                    //sending data to the Login in UserManagment service
                    //send http post request to http client
                    //send to the url must be appended
                    //parse model to json
                    var result = await _http.PostAsync("Login", Log.AsJson());


                    // if case is right
                    if (result.IsSuccessStatusCode)
                    {
                        var response = result.Content.ReadAsStringAsync();//read the response send by the usermanagent api
                        return new Response { status = "Succes", message = response.ToString() };   // required token here
                    }
                    //if case is some issue
                    else
                    {
                        return new Response { status = "Error", message = "Not Logged In Successfully" };
                    }
                }
                else
                {
                    return new Response { status = "Error", message = "Username of Password Must not be empty" };
                }
            }

        }

    }
}
