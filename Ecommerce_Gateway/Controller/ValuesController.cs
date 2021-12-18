using Ecommerce_Gateway.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Gateway.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IHubContext<InformHub, IHubClient> _informHub;
        public ValuesController(IHubContext<InformHub, IHubClient> hubContext)
        {
            _informHub = hubContext;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _informHub.Clients.All.InformClient(value);
        }
    }
}
