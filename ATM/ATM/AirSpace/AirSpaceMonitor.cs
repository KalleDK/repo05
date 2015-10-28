using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.AirSpace
{
    public class AirSpaceMonitor : IAirSpaceMonitor
    {
        private IAirspaceModel _airspace;

        public IAirspaceModel AirSpace
        {
            get
            {
                return _airspace;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                _airspace = value;
            }
        }

        private readonly Dictionary<string, Plane> _planesInAirSpace;

        public AirSpaceMonitor() : this(new AirspaceModel(new Coordinate { X = 10000, Y = 10000, Z = 500 }, new Coordinate { X = 90000, Y = 90000, Z = 20000 }))
        {
            
        }

        public AirSpaceMonitor(IAirspaceModel airspace)
        {
            _planesInAirSpace = new Dictionary<string, Plane>();
            AirSpace = airspace;
        } 

        public List<Plane> CheckAirSpace(List<PlaneObservation> newObservations)
        {
            
            foreach (var planeOb in newObservations)
            {
                if (AirSpace.Contains(planeOb.ObservedPosition.Coordinate))
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
    }
}