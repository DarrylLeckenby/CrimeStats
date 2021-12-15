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
    public class LocationHelper
    {

        public static async Task<LocationLookup> getLocation(string postcode) {

            LocationLookup objLocationLookup = new LocationLookup();

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"https://api.postcodes.io/postcodes/{postcode}");

                if (response.IsSuccessStatusCode)
                {

                    string strResponse = await response.Content.ReadAsStringAsync();
                    PostcodeIoResponse r = JsonConvert.DeserializeObject<PostcodeIoResponse>(strResponse);

                    objLocationLookup.Location = new Location
                    {
                        Name = r.result.postcode,
                        Latitude = r.result.latitude,
                        Longitude = r.result.longitude
                    };

                    objLocationLookup.Success = true;

                }
                else
                {
                    objLocationLookup.Success = false;
                    objLocationLookup.ErrorMessage = "Failed to get response from postcodes.io";
                }

                return objLocationLookup;

            }

        }

    }

}