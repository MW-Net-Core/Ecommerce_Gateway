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
        IConfigurationServices _config;
        HttpClient _http;
        

        public AccountController(IConfigurationServices config,  HttpClient http)
        {
            _config = config;
            _http = http;
        }

        [HttpPost]
        public async Task<Response> Register(Register reg)
        {

            using (_http)
            {
                _http.BaseAddress = new Uri(_config.UserMangementBaseUri.ToString());   // path to send the post data
                _http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));    // data type must be json what to send

                if (reg.Password.Equals(reg.ConfirmPassword))   //checking password
                {
                    var payload = new Dictionary<string, string>
                    {
                      {"UserName", reg.UserName},
                      {"Email", reg.Email},
                      {"Password", reg.Password}
                    };

                    string strPayload = JsonConvert.SerializeObject(payload);   // data conversion to json
                    HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    var result = await _http.PostAsync("/Authenticate/Register", c);   //await used as its async (hit on register method of usermanagement service which gives a response)
                
                    if(result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();    //get data from user managment api on successfully registration of the user
                        return new Response { status = "Sucess" , message = "Client registered sucessfully" };

                    } else
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










    }
}
