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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrimeStatsAPI.Controllers
{

    [Route("[controller]")]
    public class CrimeLookup : Controller
    {

        [HttpGet("{lat}/{lon}")]
        public async Task<IActionResult> Get(double lat = 53.9784, double lon = -1.566635)
        {

            CrimeReportsLookup objCrimeReportsLookup = await CrimeReportsHelper.getCrimeReports(lat, lon);
            return new JsonResult(objCrimeReportsLookup);

        }

    }

}