using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using CrimeStatsAPI.DataModels;
using CrimeStatsAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrimeStatsAPI.Controllers
{

    [Route("[controller]")]
    public class PostcodeLookup : Controller
    {

        [HttpGet("{postcode}")]
        public async Task<IActionResult> Get(string postcode = "HG31UD")
        {

            LocationLookup objLocationLookup = await LocationHelper.getLocation(postcode);

            return new JsonResult(objLocationLookup);

        }

    }
}
