using System;

namespace ATM.Models
{
    public struct Position
    {
        public DateTime Timestamp { get; set; }
        public Coordinate Coordinate { get; set; }
    }
}