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


        //Skal første eller anden position i arrayet være de nye koordinater?? 
        public Coordinate[] Position { get; set; }

        public double? Speed { get; set; }

        public double? Compass { get; set; }
    }

    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
