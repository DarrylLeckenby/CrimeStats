using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeStatsAPI.DataModels
{
    public class CrimeReportsLookup
    {

        public CrimeReportsLookup() {
            CrimeReports = new CrimeReports();
        }

        public Boolean Success { get; set; }
        public String ErrorMessage { get; set; }
        public CrimeReports CrimeReports { get; set; }
    }
}
