using CrimeStatsAPI.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CrimeStatsAPI.Helpers
{
    public class CrimeReportsHelper
    {

        public static async Task<CrimeReportsLookup> getCrimeReports(double lat = 53.9784, double lon = -1.566635)
        {

            CrimeReportsLookup objCrimeReportsLookup = new CrimeReportsLookup();

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"https://data.police.uk/api/crimes-street/all-crime?lat={lat}&lng={lon}");

                if (response.IsSuccessStatusCode)
                {
                    string strResponse = await response.Content.ReadAsStringAsync();
                    CrimeReports rs = new CrimeReports();
                    rs.Reports = CrimeReport.DeserializedJson(strResponse);

                    objCrimeReportsLookup.CrimeReports.Reports = CrimeReport.DeserializedJson(strResponse);
                    objCrimeReportsLookup.Success = true;
                }
                else
                {
                    objCrimeReportsLookup.Success = false;
                    objCrimeReportsLookup.ErrorMessage = "Crime reports source could not be contacted";
                }

                return objCrimeReportsLookup;

            }

        }

    }

}