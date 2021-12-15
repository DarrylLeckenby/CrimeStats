using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeStatsAPI.DataModels
{
    public class PostcodeIoResponse
    {
        public int status { get; set; }
        public PostcodeInfo result { get; set; }
    }

    public class PostcodeInfo {
        public Double longitude { get; set; }
        public Double latitude { get; set; }
        public String admin_district { get; set; }
        public String postcode { get; set; }
    }
}