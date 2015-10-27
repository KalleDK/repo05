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

            List<Plane> PlanesPressentInAirspace = new List<Plane>();

            foreach (Plane plane in toCalculate)
            {
                if ((plane.Position[0].X > Min.X && plane.Position[0].X < Max.X) &&
                    (plane.Position[0].Y > Min.Y && plane.Position[0].Y < Max.Y) &&
                    (plane.Position[0].Z > Min.Z && plane.Position[0].Z < Max.Z))
                {
                    PlanesPressentInAirspace.Add(plane);
                }

                
            }

            return PlanesPressentInAirspace;
        }
    }
}