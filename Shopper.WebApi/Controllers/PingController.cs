using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.WebApi.Controllers
{
    public class PingController
    {
        // GET api/ping
        [HttpGet("api/ping")]
        public string Ping()
        {
            return "Pong";
        }
    }
}
