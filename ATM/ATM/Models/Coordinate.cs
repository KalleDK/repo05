namespace ATM.Models
{
    public class Coordinate
    {
        public Coordinate() : this(0, 0, 0)
        {
            
        }

        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

    }
}