using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeStatsAPI.DataModels
{
    public class CrimeReports
    {

        public ICollection<CrimeReport> Reports { get; set; }
   
    }
}