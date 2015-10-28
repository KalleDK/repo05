using System;

namespace ATM.Models
{
    public class AirspaceModel
    {
        public AirspaceModel(Coordinate minCoordinate, Coordinate maxCoordinate)
        {
            UpdateCoordinates(minCoordinate, maxCoordinate);
        }

        private Coordinate _minCoordinate;
        private Coordinate _maxCoordinate;

        public void UpdateCoordinates(Coordinate minCoordinate, Coordinate maxCoordinate)
        {
            if (IsValidCoordinateSet(minCoordinate, maxCoordinate))
            {
                _minCoordinate = minCoordinate;
                _maxCoordinate = maxCoordinate;
            }
            else
            {
                throw new ArgumentException("All values in minCoordinate need to be less or equal to maxCoordinate");
            }
        }

        private static bool IsValidCoordinateSet(Coordinate minCoordinate, Coordinate maxCoordinate)
        {
            if (minCoordinate == null || maxCoordinate == null)
                return false;

            return minCoordinate.X <= maxCoordinate.X && minCoordinate.Y <= maxCoordinate.X && minCoordinate.Z <= maxCoordinate.Z;
        }

        public Coordinate MinCoordinate
        {
            get
            {
                return _minCoordinate;
            }
            set
            {
                UpdateCoordinates(value, MaxCoordinate);
            }
        }

        public Coordinate MaxCoordinate
        {
            get
            {
                return _maxCoordinate;
            }
            set
            {
                UpdateCoordinates(MinCoordinate, value);
            }
        }

        public bool Contains(Coordinate coordinate)
        {
            return (coordinate.X >= MinCoordinate.X && coordinate.X <= MaxCoordinate.X) &&
               (coordinate.Y >= MinCoordinate.Y && coordinate.Y <= MaxCoordinate.Y) &&
               (coordinate.Z >= MinCoordinate.Z && coordinate.Z <= MaxCoordinate.Z);
        }
    }
}
