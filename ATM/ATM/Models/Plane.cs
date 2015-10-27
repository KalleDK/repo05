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

<<<<<<< HEAD

        //Skal første eller anden position i arrayet være de nye koordinater?? 
        public Coordinate[] Position { get; set; }

        public double? Speed { get; set; }

        public double? Compass { get; set; }
=======
        public List<Position> Positions { get; set; } = new List<Position>();

        public double? Speed { get; set; }

        public double? Compass { get; set; }

    }

    public struct Position
    {
        public DateTime Timestamp { get; set; }
        public Coordinate Coordinate { get; set; }
>>>>>>> 5d2273b5f0d54bd60b1dfd81ad971380e0749342
    }

    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
