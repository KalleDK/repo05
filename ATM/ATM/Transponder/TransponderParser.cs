using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logging;
using ATM.Models;

namespace ATM.Transponder
{
    public class TransponderParser : ITransponderParser
    {
        private readonly ILogger _logger = new Logger(typeof(TransponderParser));
        public List<PlaneObservation> ParseRawData(List<string> rawData)
        {
            var parsedPlanes = new List<PlaneObservation>();
            char[] seperators = {';'};
            foreach (var entry in rawData)
            {
                var parts = entry.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                if(parts.Length != 5)
                    throw new NotSupportedException();
                var newPlane = new PlaneObservation
                {
                    Tag = parts[0],
                    ObservedPosition = new Position
                    {
                    Coordinate = new Coordinate { X = int.Parse(parts[1]), Y = int.Parse(parts[2]), Z = int.Parse(parts[3]) },
                    Timestamp = ParseDateTime(parts[4])
                    }
                };
                parsedPlanes.Add(newPlane);
            }
            return parsedPlanes;
        }

        public DateTime ParseDateTime(string rawDate)
        {

            const string pattern = "yyyyMMddHHmmssfff";

            DateTime parsedDate;

            if (!DateTime.TryParseExact(rawDate, pattern, null, DateTimeStyles.None, out parsedDate))
            {
                throw new NotSupportedException();
            }

            _logger.Debug("Date: " + parsedDate.ToLongDateString());
            _logger.Debug("Time: " + parsedDate.ToLongTimeString());
            _logger.Debug(("Milli: " + $"{parsedDate.Millisecond}"));

            return parsedDate;
        }
    }
}
