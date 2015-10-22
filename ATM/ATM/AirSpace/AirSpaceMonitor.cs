using System.Collections.Generic;
using ATM.Models;

namespace ATM.AirSpace
{
    public class AirSpaceMonitor : IAirSpaceMonitor
    {
        public Coordinate Min { get; set; }
        public Coordinate Max { get; set; }
        public List<Plane> CheckAirSpace(List<Plane> toCalculate)
        {

            List<Plane> toReturn = new List<Plane>();

            foreach (Plane _plane in toCalculate)
            {
                if()
            }
        }
    }
}