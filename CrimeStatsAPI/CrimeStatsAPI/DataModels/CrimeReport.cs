using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeStatsAPI.DataModels
{
    public class CrimeReport
    {

        public string category { get; set; }
        public Location location { get; set; }
        public CrimeOutcome outcome { get; set; }
        public string month { get; set; }

        public static CrimeReport[] DeserializedJson(string responseJson)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Deserialize<CrimeReport[]>(responseJson);
        }

    }

}
