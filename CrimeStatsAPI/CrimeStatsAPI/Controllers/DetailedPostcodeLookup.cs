using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrimeStatsAPI.DataModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrimeStatsAPI.Controllers
{
    [Route("[controller]")]
    public class DetailedPostcodeLookup : Controller
    {
     
        [HttpGet("{postcode}")]
        public async Task<IActionResult> Get(string postcode = "HG31UD")
        {

            DataModels.DetailedPostcodeLookup objDetailedPostcodeLookup = new DataModels.DetailedPostcodeLookup();

            LocationLookup objLocationLookup = await Helpers.LocationHelper.getLocation(postcode);

            if (objLocationLookup.Success) {

                //Place the location details into the detailed lookup response
                objDetailedPostcodeLookup.Location = objLocationLookup.Location;

                //Now find the crime reports
                CrimeReportsLookup objCrimeReportsLookup = await Helpers.CrimeReportsHelper.getCrimeReports(objLocationLookup.Location.Latitude, objLocationLookup.Location.Longitude);

                if (objCrimeReportsLookup.Success) {

                    objDetailedPostcodeLookup.CrimeReports = objCrimeReportsLookup.CrimeReports;
                    objDetailedPostcodeLookup.Success = true;

                } else {

                    objDetailedPostcodeLookup.Success = false;
                    objDetailedPostcodeLookup.ErrorMessage = "I could not fetch the crime reports. Sub-error was: " + objCrimeReportsLookup.ErrorMessage;

                }
            
            } else {

                objDetailedPostcodeLookup.Success = false;
                objDetailedPostcodeLookup.ErrorMessage = "I could not work out the location co-ordinates. Sub-error was: " + objLocationLookup.ErrorMessage;

            }

            return new JsonResult(objDetailedPostcodeLookup);

        }

    }
}
