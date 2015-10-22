using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.AirSpace
{
    interface IAirSpaceMonitor
    {
        Coordinate Min { get; set; }
        Coordinate Max { get; set; }

        List<Plane> CheckAirSpace(List<Plane> toCalculate);
    }
}
