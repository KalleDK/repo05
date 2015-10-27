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
        public List<Plane> ParseRawData(List<string> rawData)
        {
            List<Plane> parsedPlanes = new List<Plane>();
            char[] seperators = {';'};
            foreach (var entry in rawData)
            {
                var parts = entry.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                var newPlane = new Plane() { Tag = parts[0] };
                var position = new Position
                {
                    Coordinate =
                        new Coordinate() {X = int.Parse(parts[1]), Y = int.Parse(parts[2]), Z = int.Parse(parts[3])},
                    Timestamp = ParseDateTime(parts[4])
                };
                newPlane.Positions.Add(position);
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
