using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.AirSpace
{
    public class AirSpaceMonitor : IAirSpaceMonitor
    {
        public Coordinate Min { get; private set; }
        public Coordinate Max { get; private set; }

        //public List<Plane> _PlanesInAirSpace;
        private readonly Dictionary<string, Plane> _planesInAirSpace;

        public AirSpaceMonitor() : this(new Coordinate { X = 10000, Y = 10000, Z = 500 }, new Coordinate { X = 90000, Y = 90000, Z = 20000 })
        {
            
        }

        public AirSpaceMonitor(Coordinate min, Coordinate max)
        {
            _planesInAirSpace = new Dictionary<string, Plane>();

            Min = min;
            Max = max;
        } 
        public List<Plane> CheckAirSpace(List<PlaneObservation> newObservations)
        {
            
            foreach (var planeOb in newObservations)
            {
                if (CoordinateInAirSpace(planeOb.ObservedPosition.Coordinate))
                {
                    if (_planesInAirSpace.ContainsKey(planeOb.Tag))
                    {
                        _planesInAirSpace[planeOb.Tag].Positions.Insert(0,planeOb.ObservedPosition);
                    }
                    else
                    {
                        _planesInAirSpace.Add(planeOb.Tag, new Plane {Tag = planeOb.Tag, Positions = {planeOb.ObservedPosition}});
                    }
                }
                else
                {
                    if (_planesInAirSpace.ContainsKey(planeOb.Tag))
                    {
                        _planesInAirSpace.Remove(planeOb.Tag);
                    }
                }
            }

            return _planesInAirSpace.Values.ToList();
        }


        //Returns true if planeobservation is in Airspace
        private bool CoordinateInAirSpace(Coordinate coordinate)
        {
            return (coordinate.X >= Min.X && coordinate.X <= Max.X) &&
                (coordinate.Y >= Min.Y && coordinate.Y <= Max.Y) &&
                (coordinate.Z >= Min.Z && coordinate.Z <= Max.Z);

        }
    }
}