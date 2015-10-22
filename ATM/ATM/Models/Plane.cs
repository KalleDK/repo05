using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class Plane
    {
        string Tag { get; set; }

        Coordinate[] Position { get; set; }

        double? Speed { get; set; }

        double? Compass { get; set; }
    }

    public struct Coordinate
    {
        int X { get; set; }
        int Y { get; set; }
        int Z { get; set; }
    }
}
