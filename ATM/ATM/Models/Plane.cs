using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    class Plane
    {
        string Tag { get; set; }

        Coordinate[] Position { get; set; }

        double? Speed { get; set; }

        double? Compass { get; set; }
    }

    struct Coordinate
    {
        int x { get; set; }
        int y { get; set; }
        int z { get; set; }
    }
}
