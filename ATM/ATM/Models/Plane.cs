using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{

    public abstract class PlaneFactory
    {
        public static Plane MakePlane(string tag, int x, int y, int z, DateTime timestamp)
        {
            return new Plane
            {
                Tag = tag,
                Positions =
                {
                    new Position
                    {
                        Coordinate = new Coordinate
                        {
                            X = x,
                            Y = y,
                            Z = z,
                        },
                        Timestamp = timestamp,
                    }
                }
            };
        }
    }

    public class Plane
    {
        public string Tag { get; set; }
        
        public List<Position> Positions { get; set; } = new List<Position>();

        public double? Speed { get; set; }

        public double? Compass { get; set; }

    }

}
