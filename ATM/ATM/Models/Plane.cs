using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class Plane
    {
        public string Tag { get; set; }

        
        public List<Position> Positions { get; set; } = new List<Position>();

        public double? Speed { get; set; }

        public double? Compass { get; set; }

    }

    public struct PlaneObservation
    {
        public string Tag { get; set; }
        public Position ObservedPosition { get; set; }
        
    }

    public struct Position
    {
        public DateTime Timestamp { get; set; }
        public Coordinate Coordinate { get; set; }
    }

    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
