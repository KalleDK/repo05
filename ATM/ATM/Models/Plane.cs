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

}
